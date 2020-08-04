using Mosdong.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Mosdong.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser {get; set;}

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double OrderTotalOriginal { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "주문총액")]
        public double OrderTotal { get; set; }

        [Required]
        [Display(Name="픽업시간")]
        public DateTime PickUpTime { get; set; }

        [Required]
        [NotMapped]
        public DateTime PickUpDate { get; set; }

        [Display(Name ="쿠폰코드")]
        public string CouponCode { get; set; }
        public double CouponCodeDiscount { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string Comments { get; set; }

        [Display(Name = "배송/픽업 선택")]
        public string DeliveryOrPickup { get; set; }

        [Display(Name = "계좌이체 이름")]
        public string AccountName { get; set; }


    }
}
