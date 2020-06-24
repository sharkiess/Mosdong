using Mosdong.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
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

        [Display(Name = "상품홍보")]
        public string ProductAd { get; set; }
        public enum EProductAd { 홍보없음 = 0, 신상품 = 1, 인기상품 = 2, 추천상품 = 3 }

        [Range(1, int.MaxValue, ErrorMessage = "판매단위는 1 이상이어야합니다.")]
        public int ProductUnitQuantity { get; set; }

        public enum EProductUnit
        {
            [EnumMember(Value = "개")]
            개,
            [EnumMember(Value = "팩")]
            팩,
            [EnumMember(Value = "그램")]
            그램,
            [EnumMember(Value = "키로")]
            키로,
            [EnumMember(Value = "박스")]
            박스,
            [EnumMember(Value = "세트")]
            세트,
            [EnumMember(Value = "마리")]
            마리,
            [EnumMember(Value = "포기")]
            포기
        }

        [Display(Name = "판매단위")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EProductUnit ProductUnit { get; set; }

        [Display(Name = "상품표시옵션")]
        public bool IsNotVisible { get; set; }
        public bool IsStockUnlimited { get; set; }
        
        [Display(Name = "상품이미지")]
        public string Image { get; set; }

        [RegularExpression(@"^[1-9]\d*$", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "IsRequired")]
        [Display(Name = "카테고리 1")]
        public int CategoryId { get; set; }

       
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "카테고리 2")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        [Display(Name = "카테고리 3")]
        public int MiniCategoryId { get; set; }

        [ForeignKey("MiniCategoryId")]
        public virtual MiniCategory MiniCategory { get; set; }

    }
}
