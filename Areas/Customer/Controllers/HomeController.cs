using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mosdong.Data;
using Mosdong.Models;
using Mosdong.Models.ViewModels;
using Newtonsoft.Json.Schema;
using Mosdong.Utility;

namespace Mosdong.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                ProductItem = await _db.ProductItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync(),
                Category = await _db.Category.ToListAsync(),
                Coupon = await _db.Coupon.Where(c => c.IsActive == true).ToListAsync()
            };

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim!=null)
            {
                var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShopingCartCount, cnt);
            }

            return View(IndexVM);
        }

        public async Task<IActionResult> Category2(int id)
        {
            var productItems = await _db.ProductItem.Include(m => m.Category).Include(m => m.SubCategory).Include(m => m.MiniCategory).Where(m => m.SubCategoryId == id).ToListAsync();
            return View(productItems);
        }

        public async Task<IActionResult> Category3(int id)
        {
            IndexViewModel IndexVM = new IndexViewModel()
            {
                ProductItem = await _db.ProductItem.Include(m => m.Category).Include(m => m.SubCategory).ToListAsync()            };

            return View(IndexVM);
        }


        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            var productItemFromDb = await _db.ProductItem.Include(m => m.Category).Include(m => m.SubCategory).Include(m => m.MiniCategory).Where(m => m.Id == id).FirstOrDefaultAsync();

            ShoppingCart cartObj = new ShoppingCart()
                {
                    ProductItem = productItemFromDb,
                    ProductItemId = productItemFromDb.Id
                };

            return View(cartObj);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(ShoppingCart CartObject)
        {
            CartObject.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = await _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId
                                            && c.ProductItemId == CartObject.ProductItemId).FirstOrDefaultAsync();
                if (cartFromDb == null)
                {
                    await _db.ShoppingCart.AddAsync(CartObject);
                }
                else
                {
                    cartFromDb.Count = cartFromDb.Count + CartObject.Count;
                }
                await _db.SaveChangesAsync();
                //장바구니에 담긴 아이템 숫자
                var count = _db.ShoppingCart.Where(c => c.ApplicationUserId == CartObject.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShopingCartCount, count);

                return RedirectToAction("Index");
            }
            else
            {
                var productItemFromDb = await _db.ProductItem.Include(m => m.Category).Include(m => m.SubCategory).Where(m => m.Id == CartObject.ProductItemId).FirstOrDefaultAsync();

                ShoppingCart cartObj = new ShoppingCart()
                {
                    ProductItem = productItemFromDb,
                    ProductItemId = productItemFromDb.Id
                };

                return View(cartObj);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
