﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mosdong.Models;
using Mosdong.Models.ViewModels;

namespace Mosdong.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category {get; set;}
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<MiniCategory> MiniCategory { get; set; }
        public DbSet<ProductItemModel> ProductItem { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
