using Mosdong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Utility
{
    public class SD
    {
        public const string DefaultImage = "default_image.png";
        public const string ManagerUser = "Manager";
        public const string PartnerUser  = "Partner";
        public const string FrontDeskUser = "FrontDesk";
        public const string CustomerEndUser = "Customer";

        public const string ssShopingCartCount = "ssCartCount";
        public const string ssCouponCode = "ssCouponCode";

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static double DiscountedPrice(Coupon couponFromDb, double OriginalOrderTotal)
        {
            if(couponFromDb==null)
            {
                return OriginalOrderTotal;
            }
            else
            {
                if (couponFromDb.MinimumAmount > OriginalOrderTotal)
                {
                    return OriginalOrderTotal;
                }
                else
                {
                    //everything is valid
                    if(Convert.ToInt32(couponFromDb.CouponType) == (int)Coupon.ECouponType.루블)
                    {
                        return Math.Round(OriginalOrderTotal - couponFromDb.Discount, 2);
                    }
                    
                    if (Convert.ToInt32(couponFromDb.CouponType) == (int)Coupon.ECouponType.퍼센트)
                    {
                        return Math.Round(OriginalOrderTotal - (OriginalOrderTotal * couponFromDb.Discount/100), 2);
                    }
                }
            }

            return OriginalOrderTotal;
        }
    }
}
  