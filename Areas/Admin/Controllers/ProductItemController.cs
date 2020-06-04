using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mosdong.Data;
using Mosdong.Models.ViewModels;
using Mosdong.Utility;

namespace Mosdong.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                ProductItem = new Models.ProductItemModel()
            };
        }
        
        public async Task<IActionResult> Index()
        {
            var productItems = await _db.ProductItem.Include(m=>m.Category).Include(m => m.SubCategory).ToListAsync();
            return View(productItems);
        }

        //GET - Create
        public IActionResult Create() {
            return View(ProductItemVM);
        }

        //POST - Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            ProductItemVM.ProductItem.SubCategoryId = Convert.ToInt32(Request.Form["SubCategoryId"].ToString());

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

                using (var filesStream = new FileStream(Path.Combine(uploads, ProductItemVM.ProductItem.Id + extension), FileMode.Create))
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

    }
}