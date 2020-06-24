﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mosdong.Data;
using Mosdong.Models;
using Mosdong.Models.ViewModels;
using Mosdong.Utility;

namespace Mosdong.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.ManagerUser)]
    public class ProductItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public ProductItemViewModel ProductItemVM { get; set; }
        
        public ProductItemController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            
            ProductItemVM = new ProductItemViewModel()
            {
                Category = _db.Category,
                SubCategory = _db.SubCategory,
                MiniCategory = _db.MiniCategory,
                ProductItem = new Models.ProductItemModel()
            };
        }
        public async Task<IActionResult> Index()
        {
            var productItems = await _db.ProductItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync();
            return View(productItems);
        }

        //GET - Create
        public IActionResult Create() {
            
            List<Category> categoryList = new List<Category>();
            categoryList = (from category in _db.Category
                            select category).ToList();
            categoryList.Insert(0, new Category { Id = 0, Name = "Select" });
            ViewBag.ListofCategory = categoryList;

            return View(ProductItemVM);
        }

        //POST - Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            
            ProductItemVM.ProductItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
            ProductItemVM.ProductItem.MiniCategoryId = Convert.ToInt32(Request.Form["MiniCategoryId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(ProductItemVM);
            }

            _db.ProductItem.Add(ProductItemVM.ProductItem);
            await _db.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var productItemFromDb = await _db.ProductItem.FindAsync(ProductItemVM.ProductItem.Id);

            if (files.Count > 0)
            {
                //file has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, webRootPath + @"\images\product_images\" + ProductItemVM.ProductItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                productItemFromDb.Image = @"\images\product_images\" + ProductItemVM.ProductItem.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default image
                var uploads = Path.Combine(webRootPath, @"images\product_images\" + SD.DefaultImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\product_images\" + ProductItemVM.ProductItem.Id + ".png");
                productItemFromDb.Image = @"\images\product_images\" + ProductItemVM.ProductItem.Id + ".png";

            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //GET - Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductItemVM.ProductItem = await _db.ProductItem.Include(p => p.Category).Include(p => p.SubCategory).SingleOrDefaultAsync(p => p.Id == id);

            List<Category> categoryList = new List<Category>();
            categoryList = (from category in _db.Category
                            select category).ToList();
            categoryList.Insert(0, new Category { Id = 0, Name = "Select" });
            ViewBag.ListofCategory = categoryList;

            List<SubCategory> subCategoryList = new List<SubCategory>();
            subCategoryList = (from subCategory in _db.SubCategory
                               where subCategory.CategoryId == ProductItemVM.ProductItem.CategoryId
                            select subCategory).ToList();
            subCategoryList.Insert(0, new SubCategory { Id = 0, Name = "Select" });
            ViewBag.ListofSubCategory = subCategoryList;

            List<MiniCategory> miniCategoryList = new List<MiniCategory>();
            miniCategoryList = (from miniCategory in _db.MiniCategory
                                where miniCategory.SubCategoryId == ProductItemVM.ProductItem.SubCategoryId
                                select miniCategory).ToList();
            miniCategoryList.Insert(0, new MiniCategory { Id = 0, Name = "Select" });
            ViewBag.ListofMiniCategory = miniCategoryList;


            if (ProductItemVM.ProductItem == null)
            {
                return NotFound();
            }
            return View(ProductItemVM);
        }

        //POST - Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ProductItemVM.ProductItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());
            ProductItemVM.ProductItem.MiniCategoryId = Convert.ToInt32(Request.Form["MiniCategoryId"].ToString());


            if (!ModelState.IsValid)
            {
                ProductItemVM.SubCategory = await _db.SubCategory.Where(s => s.CategoryId == ProductItemVM.ProductItem.CategoryId).ToListAsync();
                ProductItemVM.MiniCategory = await _db.MiniCategory.Where(m => m.SubCategoryId == ProductItemVM.ProductItem.SubCategoryId).ToListAsync();
                return View(ProductItemVM);
            }

            //Work on the image saving section

            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var productItemFromDb = await _db.ProductItem.FindAsync(ProductItemVM.ProductItem.Id);

            if (files.Count > 0)
            {
                //New file has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, productItemFromDb.Image.TrimStart('\\'));

                if(System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //we will upload the image
                using (var filesStream = new FileStream(Path.Combine(uploads, webRootPath + @"\images\product_images\" + ProductItemVM.ProductItem.Id + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                productItemFromDb.Image = @"\images\product_images\" + ProductItemVM.ProductItem.Id + extension_new;
            }

            productItemFromDb.Name = ProductItemVM.ProductItem.Name;
            productItemFromDb.Price = ProductItemVM.ProductItem.Price;
            productItemFromDb.SalePrice = ProductItemVM.ProductItem.SalePrice;
            productItemFromDb.Description = ProductItemVM.ProductItem.Description;
            productItemFromDb.StockAvailabilityNum = ProductItemVM.ProductItem.StockAvailabilityNum;
            productItemFromDb.StockAvailability = ProductItemVM.ProductItem.StockAvailability;
            productItemFromDb.ProductUnitQuantity = ProductItemVM.ProductItem.ProductUnitQuantity;
            productItemFromDb.ProductUnit = ProductItemVM.ProductItem.ProductUnit;
            productItemFromDb.IsNotVisible = ProductItemVM.ProductItem.IsNotVisible;
            productItemFromDb.ProductAd = ProductItemVM.ProductItem.ProductAd;
            productItemFromDb.IsStockUnlimited = ProductItemVM.ProductItem.IsStockUnlimited;
            productItemFromDb.CategoryId = ProductItemVM.ProductItem.CategoryId;
            productItemFromDb.SubCategoryId = ProductItemVM.ProductItem.SubCategoryId;
            productItemFromDb.MiniCategoryId = ProductItemVM.ProductItem.MiniCategoryId;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET - Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductItemVM.ProductItem = await _db.ProductItem.Include(p => p.Category).Include(p => p.SubCategory).SingleOrDefaultAsync(p => p.Id == id);

            if (ProductItemVM.ProductItem == null)
            {
                return NotFound();
            }
            return View(ProductItemVM);
        }

        //POST - Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string webRootPath = _hostEnvironment.WebRootPath;
            ProductItemModel productItem = await _db.ProductItem.FindAsync(id);

            if (productItem != null)

            {
                var imagePath = Path.Combine(webRootPath, productItem.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.ProductItem.Remove(productItem);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }
}