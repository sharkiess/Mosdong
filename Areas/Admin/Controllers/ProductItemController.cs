using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mosdong.Data;
using Mosdong.Models.ViewModels;

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
    }
}