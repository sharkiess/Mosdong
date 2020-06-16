using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="카테고리 1")]
        [Required(ErrorMessage = "필수 입력 사항입니다.")]
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
