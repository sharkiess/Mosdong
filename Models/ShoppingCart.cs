using Mosdong.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int ProductItemId { get; set; }

        [NotMapped]
        [ForeignKey("ProductItemId")]
        public virtual ProductItemModel ProductItem { get; set; }

        [Range(1,int.MaxValue,ErrorMessage ="1개 이상 장바구니에 담을 수 있습니다.")]
        public int Count { get; set; }

    }
}
