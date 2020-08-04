using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mosdong.Data;
using Mosdong.Models.ViewModels;
using Mosdong.Utility;

namespace Mosdong.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public OrderDetailsCart detailsCart { get; set; }

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            detailsCart = new OrderDetailsCart()
            {
                OrderHeader = new Models.OrderHeader()
            };

            detailsCart.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                detailsCart.ListCart = cart.ToList();
            }

            foreach (var list in detailsCart.ListCart)
            {
                list.ProductItem = await _db.ProductItem.FirstOrDefaultAsync(m => m.Id == list.ProductItemId);
                detailsCart.OrderHeader.OrderTotal =
                    detailsCart.OrderHeader.OrderTotal + (list.ProductItem.Price * list.Count);
                /*
                list.ProductItem.Description = SD.ConvertToRawHtml(list.ProductItem.Description);
                if (list.ProductItem.Description.Length > 100)
                {
                    list.ProductItem.Description = list.ProductItem.Description.Substring(0, 99) + "...";
                }
                */
            }
            detailsCart.OrderHeader.OrderTotalOriginal = detailsCart.OrderHeader.OrderTotal;
            
            if(HttpContext.Session.GetString(SD.ssCouponCode)!=null)
            {
                detailsCart.OrderHeader.CouponCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.Coupon.Where(c => c.Name.ToLower() == detailsCart.OrderHeader.CouponCode.ToLower()).FirstOrDefaultAsync();
                detailsCart.OrderHeader.OrderTotal = SD.DiscountedPrice(couponFromDb, detailsCart.OrderHeader.OrderTotalOriginal); 
            }

            return View(detailsCart);
        }

        public async Task<IActionResult> Summary()
        {
            detailsCart = new OrderDetailsCart()
            {
                OrderHeader = new Models.OrderHeader()
            };

            detailsCart.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var cart = _db.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                detailsCart.ListCart = cart.ToList();
            }

            foreach (var list in detailsCart.ListCart)
            {
                list.ProductItem = await _db.ProductItem.FirstOrDefaultAsync(m => m.Id == list.ProductItemId);
                detailsCart.OrderHeader.OrderTotal =
                    detailsCart.OrderHeader.OrderTotal + (list.ProductItem.Price * list.Count);
                /*
                list.ProductItem.Description = SD.ConvertToRawHtml(list.ProductItem.Description);
                if (list.ProductItem.Description.Length > 100)
                {
                    list.ProductItem.Description = list.ProductItem.Description.Substring(0, 99) + "...";
                }
                */
            }
            detailsCart.OrderHeader.OrderTotalOriginal = detailsCart.OrderHeader.OrderTotal;

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                detailsCart.OrderHeader.CouponCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.Coupon.Where(c => c.Name.ToLower() == detailsCart.OrderHeader.CouponCode.ToLower()).FirstOrDefaultAsync();
                detailsCart.OrderHeader.OrderTotal = SD.DiscountedPrice(couponFromDb, detailsCart.OrderHeader.OrderTotalOriginal);
            }

            return View(detailsCart);
        }

        public IActionResult AddCoupon()
        {
            if(detailsCart.OrderHeader.CouponCode==null)
            {
                detailsCart.OrderHeader.CouponCode = "";
            }
            HttpContext.Session.SetString(SD.ssCouponCode, detailsCart.OrderHeader.CouponCode);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveCoupon()
        {

            HttpContext.Session.SetString(SD.ssCouponCode, string.Empty);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Plus(int cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            cart.Count += 1;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus (int cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            if(cart.Count==1)
            {
                _db.ShoppingCart.Remove(cart);
                await _db.SaveChangesAsync();

                var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssShopingCartCount, cnt);
            }
            else
            {
                cart.Count -= 1;
                await _db.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove (int cartId)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(c => c.Id == cartId);
            
            _db.ShoppingCart.Remove(cart);
            await _db.SaveChangesAsync();

            var cnt = _db.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssShopingCartCount, cnt);

            return RedirectToAction(nameof(Index));
        }
    }
}
