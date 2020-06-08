using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mosdong.Data;
using Mosdong.Models;
using Mosdong.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.ViewComponents
{
    public class TopNavViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public TopNavViewComponent (ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            NavListViewModel model = new NavListViewModel
            {
                NavCategory = await _db.Category.ToListAsync(),
                SubCategory = new Models.SubCategory(),
                NavSubCategory = await _db.SubCategory.OrderBy(p => p.Name).ToListAsync()
            };

            return View(model);

        }
    } 
}
