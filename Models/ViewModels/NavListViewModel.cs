using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models.ViewModels
{
    public class NavListViewModel
    {
        public IEnumerable<Category> NavCategory { get; set; }
        public SubCategory SubCategory { get; set; }
        public IEnumerable<SubCategory> NavSubCategory { get; set; }
        public MiniCategory MiniCategory { get; set; }
        public IEnumerable<MiniCategory> NavMiniCategory { get; set; }

    }
}
