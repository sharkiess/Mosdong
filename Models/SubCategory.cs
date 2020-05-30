﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mosdong.Models
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "하위 카테고리명")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "카테고리명")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
