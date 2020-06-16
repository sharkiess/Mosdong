﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mosdong.Models
{
    public class MiniCategory
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "필수 입력 사항입니다.")]
        [Display(Name = "카테고리 3")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "카테고리 2")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { get; set; }

        [Required]
        [Display(Name = "카테고리 1")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}