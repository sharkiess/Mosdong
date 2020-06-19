using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mosdong.Models.ViewModels
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string KakaoName { get; set; }

        public string StreeAddress { get; set; }

        public string DomNumber { get; set; }

        public string EntranceNumber { get; set; }

        public int FloorNumber { get; set; }

        public string AptNumber { get; set; }

        public string City { get; set; }
        public string Area { get; set; }
    }
}
