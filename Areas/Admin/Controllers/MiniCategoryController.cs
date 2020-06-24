using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Mosdong.Data;
using Mosdong.Models;
using Mosdong.Models.ViewModels;
using Mosdong.Utility;

namespace Mosdong.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class MiniCategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
 
        [TempData]
        public string StatusMessage { get; set; }

        public MiniCategoryController(ApplicationDbContext db)
        {
            _db = db;
        }



        //GET Index
        public async Task<IActionResult> Index()
        {
            var miniCategories = await _db.MiniCategory.Include(s => s.Category).Include(s => s.SubCategory).ToListAsync();
            return View(miniCategories.OrderBy(m => m.Name).ToList());
        }

        //GET - Create
        public IActionResult Create()
        {
            List<Category> categoryList = new List<Category>();
            categoryList = (from category in _db.Category
                            select category).ToList();
            categoryList.Insert(0, new Category { Id = 0, Name = "Select" });
            ViewBag.
                Category = categoryList;

            List<SubCategory> subCategoryList = new List<SubCategory>();
            subCategoryList = (from subCategory in _db.SubCategory
                               select subCategory).ToList();
            subCategoryList.Insert(0, new SubCategory { Id = 0, Name = "Select" });
            ViewBag.ListofSubCategory = subCategoryList;

            AllCategoryViewModel model = new AllCategoryViewModel
            {
                CategoryList = _db.Category.ToList(),
                SubCategoryList = _db.SubCategory.ToList(),
                MiniCategory = new Models.MiniCategory(),
                MiniCategoryList = _db.MiniCategory.OrderBy(m => m.Name).Select(m => m.Name).ToList()
            };

            return View(model);

            }

        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AllCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesMiniCategoryExists = _db.MiniCategory.Include(s => s.SubCategory).Where(s => s.Name == model.MiniCategory.Name && s.SubCategory.Id == model.MiniCategory.SubCategoryId);
                if (doesMiniCategoryExists.Count() > 0)
                {
                    //Error
                    StatusMessage = "에러: " + doesMiniCategoryExists.First().SubCategory.Name + "카테고리 내에 이미 존재하는 항목입니다. 새로운 항목을 입력하세요.";

                }
                else
                {
                    _db.MiniCategory.Add(model.MiniCategory);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }


            AllCategoryViewModel modelVM = new AllCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategoryList = await _db.SubCategory.ToListAsync(),
                MiniCategory = model.MiniCategory,
                MiniCategoryList = await _db.MiniCategory.OrderBy(m=>m.Name).Select(m=>m.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }



        [HttpPost]
        public JsonResult GetSubCategory(int id)
        {
            List<SubCategory> subCategories = _db.SubCategory.OrderBy(s=>s.Name).Where(s => s.CategoryId == id).ToList();
            return Json(subCategories);
        }

        [ActionName("GetMiniCategoryList")]
        public JsonResult GetMiniCategoryList(int id)
        {
            List<MiniCategory> miniCategories = new List<MiniCategory>();
            miniCategories = (from miniCategory in _db.MiniCategory
                              orderby miniCategory.Name
                              where miniCategory.SubCategoryId == id
                              select miniCategory).ToList();

            return Json(new SelectList(miniCategories, "Id", "Name"));
        }


        [ActionName("GetMiniCategory")]
        public JsonResult GetMiniCategory(int SubCategoryId)
        {
            List<MiniCategory> miniCategories = new List<MiniCategory>();
            miniCategories = (from miniCategory in _db.MiniCategory
                                    orderby miniCategory.Name
                                    where miniCategory.SubCategoryId == SubCategoryId
                              select miniCategory).ToList();

            //Inserting Select Item in List
            miniCategories.Insert(0, new MiniCategory { Id = 0 , Name = "Select" });

            return Json(new SelectList(miniCategories, "Id", "Name"));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var miniCategory =  await _db.MiniCategory.SingleOrDefaultAsync(m => m.Id == id);

            if (miniCategory == null)
            {
                return NotFound();
            }

            var subCategories = new SelectList(_db.SubCategory.ToList(), "Id", "Name");

            AllCategoryViewModel model = new AllCategoryViewModel
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategoryList = await _db.SubCategory.ToListAsync(),
                MiniCategory = miniCategory,
                MiniCategoryList = await _db.MiniCategory.OrderBy(m => m.Name).Select(m => m.Name).ToListAsync()
            };

            return View(model);
        }

        //POST - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AllCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesMiniCategoryExists = _db.MiniCategory.Include(s => s.SubCategory).Where(s => s.Name == model.MiniCategory.Name && s.SubCategory.Id == model.MiniCategory.SubCategoryId);
                if (doesMiniCategoryExists.Count() > 0)
                {
                    //Error
                    StatusMessage = "에러: 편집을 실행할 변경 내용이 없습니다. 목록을 누르고 리스트로 돌아갈 수 있습니다.";

                }
                else
                {
                    var miniCatFromDb = await _db.MiniCategory.FindAsync(model.MiniCategory.Id);
                    miniCatFromDb.Name = model.MiniCategory.Name;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            AllCategoryViewModel modelVM = new AllCategoryViewModel()
            {
                CategoryList = await _db.Category.ToListAsync(),
                SubCategoryList = await _db.SubCategory.ToListAsync(),
                MiniCategory = model.MiniCategory,
                MiniCategoryList = await _db.MiniCategory.OrderBy(m => m.Name).Select(m => m.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        //GET Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var miniCategory = await _db.MiniCategory.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);
            if (miniCategory == null)
            {
                return NotFound();
            }

            return View(miniCategory);
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var miniCategory = await _db.MiniCategory.Include(m => m.Category).Include(m => m.SubCategory).SingleOrDefaultAsync(m => m.Id == id);

            if (miniCategory == null)
            {
                return NotFound();
            }

            return View(miniCategory);
        }

        //POST - Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miniCategory = await _db.MiniCategory.SingleOrDefaultAsync(m => m.Id == id);
            _db.MiniCategory.Remove(miniCategory);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
