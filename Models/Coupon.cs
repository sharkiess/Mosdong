using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models
{

    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "쿠폰코드")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "할인타입")]
        public string CouponType { get; set; }
        public enum ECouponType { 퍼센트 = 0, 루블 = 1 }

        [Required]
        [Display(Name = "할인율/할인금액")]
        public double Discount { get; set; }
        [Required]
        [Display(Name = "최소결제금액")]
        public double MinimumAmount { get; set; }
        [Display(Name = "쿠폰이미지")]
        public byte[] Picture { get; set; }
        [Display(Name = "쿠폰유효여부")]
        public bool IsActive { get; set; }
    }

}
