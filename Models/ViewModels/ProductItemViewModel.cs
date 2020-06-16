using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models.ViewModels
{
    public class ProductItemViewModel
    {
        public ProductItemModel ProductItem { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<SubCategory> SubCategory { get; set; }
        public IEnumerable<MiniCategory> MiniCategory { get; set; }
    }
}
