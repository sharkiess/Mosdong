﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models.ViewModels
{
    public class MiniCategoryAndSubCategoryViewModel
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<SubCategory> SubCategoryList { get; set; }
        public MiniCategory MiniCategory { get; set; }
        public List<string> MiniCategoryList { get; set; }
        public string StatusMessage { get; set; }

    }
}