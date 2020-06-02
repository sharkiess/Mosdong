using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models
{
    public class ProductItemModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "상품명")]
        public string Name { get; set; }

        [Display(Name = "상품상세설명")]
        public string Description { get; set; }

        [Display(Name = "재고수량")]
        public int StockAvailabilityNum { get; set; }

        [Display(Name = "재고상태")]
        public string StockAvailability { get; set; }
        public enum EStockStatus { 판매중 = 0, 품절 = 1, 입고예정 = 2 }

        public string ProductUnit { get; set; }
        public enum EProductUnit {  개 = 0, 팩 = 1, 그램 = 2, 키로 = 3, 박스 = 4, 세트 = 5, 마리 = 6, 포기 = 7 }        

        public bool IsNotVisible { get; set; }

        [Display(Name = "상품이미지")]
        public string Image { get; set; }

        [Display(Name = "카테고리명")]
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "하위 카테고리명")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }


        [Range(1, int.MaxValue, ErrorMessage ="Price should be greater than ${1}")]
        [Display(Name = "가격")]
        public double Price { get; set; }


    }
}
