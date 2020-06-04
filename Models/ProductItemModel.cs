using Mosdong.Properties;
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

        
        [Display(Name = "상품명")]
        [Required(ErrorMessage = "필수입력 항목입니다.")]
        public string Name { get; set; }

        [Display(Name = "판매가격")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "IsRequired")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MustBeANumber")]
        [Range(0, double.MaxValue, ErrorMessage = "판매가격은 0루블 이상이어야합니다.")]
        public double Price { get; set; }

        [Display(Name = "할인가격")]
        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "IsRequired")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MustBeANumber")]
        [Range(0, double.MaxValue, ErrorMessage = "할인가격은 0루블 이상이어야합니다.")]
        public double SalePrice { get; set; }

        [Display(Name = "상품상세설명")]
        public string Description { get; set; }

        [Display(Name = "재고수량")]
        [Required(ErrorMessage = "수량을 입력하거나 제한없음을 선택하세요")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MustBeANumber")]
        public int StockAvailabilityNum { get; set; }

        [Display(Name = "재고상태")]
        public string StockAvailability { get; set; }
        public enum EStockStatus { 판매중 = 0, 품절 = 1, 입고예정 = 2 }

        public int ProductUnitQuantity { get; set; }

        [Display(Name = "판매단위")]
        public string ProductUnit { get; set; }
        public enum EProductUnit {  개 = 0, 팩 = 1, 그램 = 2, 키로 = 3, 박스 = 4, 세트 = 5, 마리 = 6, 포기 = 7 }        

        public bool IsNotVisible { get; set; }
        public bool IsStockUnlimited { get; set; }
        
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



    }
}
