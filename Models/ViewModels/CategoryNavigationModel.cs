using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models
{
    public class CategoryNavigationModel 
    {
    public List<Category> CategoryList { get; set; }
    public SubCategory SubCategory { get; set; }
    public List<string> CurrentSubCategoryList { get; set; }
    public int CurrentCategoryId { get; set; }


        #region Nested classes

        public class CategoryLineModel 
    {
        public int CurrentCategoryId { get; set; }
        public Category Category { get; set; }
    }

        #endregion
    }
}