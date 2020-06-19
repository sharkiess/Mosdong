using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Mosdong.Models.ViewModels;
using Mosdong.Utility;

namespace Mosdong.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "EmailRequired")]
            [EmailAddress(ErrorMessage = "EmailNotValid")]
            [Display(Name = "이메일*")]

            public string Email { get; set; }

            [Required(ErrorMessage = "영문 대소문자, 숫자, 특수문자가 각 1자리 이상 포함되어야합니다.")]
            [StringLength(20, ErrorMessage = "최소 {2}자리 이상 영문대소문자, 숫자, 특수문자가 각 1자리 이상 포함되어야합니다..", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "비밀번호*")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "비밀번호확인*")]
            [Compare("Password", ErrorMessage = "위의 비밀번호와 일치하지 않습니다.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage ="이름을 입력해주세요.")]
            [Display(Name = "이름(실명)*")]
            public string Name { get; set; }

            [Display(Name = "카톡프로필명")]
            public string KakaoName { get; set; }

            [Required(ErrorMessage = "휴대폰 번호를 입력해주세요.")]
            [DataType(DataType.PhoneNumber, ErrorMessage = "휴대폰 번호를 확인해주세요.")]
            [RegularExpression(@"((\+7)|7|(\+8)|8)[.\- ]?[0-9]{3}[.\- ]?[0-9]{3}[.\- ]?[0-9]{4}", ErrorMessage = "휴대폰 번호를 확인해주세요.")]
            [Display(Name = "휴대폰*")]
            public string PhoneNumber { get; set; }

            [Display(Name = "거주지역*")]
            public string Area { get; set; }

            [Display(Name = "거리명")]
            public string StreeAddress { get; set; }
            
            [Display(Name = "빌딩(돔)번호")]
            public string DomNumber { get; set; }
            
            [Display(Name = "입구번호")]
            public string EntranceNumber { get; set; }
            
            [Required(ErrorMessage = "엘레베이터 층수를 입력해주세요.")]
            [Display(Name = "층수*")]
            public int FloorNumber { get; set; }
            [Required(ErrorMessage = "호수를 입력해주세요.")]
            [Display(Name = "호수*")]
            public string AptNumber { get; set; }
            
            [Required(ErrorMessage = "도시명을 입력해주세요.")]
            [Display(Name = "도시명*")]
            public string City { get; set; }
                        
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            string role = Request.Form["rdUserRole"].ToString();

            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    Name = Input.Name,
                    PhoneNumber = Input.PhoneNumber,
                    KakaoName = Input.KakaoName,
                    StreeAddress = Input.StreeAddress,
                    DomNumber = Input.DomNumber,
                    EntranceNumber = Input.EntranceNumber,
                    FloorNumber = Input.FloorNumber,
                    AptNumber = Input.AptNumber,
                    City = Input.City,
                    Area = Input.Area
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if(!await _roleManager.RoleExistsAsync(SD.CustomerEndUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.CustomerEndUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.ManagerUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.FrontDeskUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.FrontDeskUser));
                    }
                    if (!await _roleManager.RoleExistsAsync(SD.PartnerUser))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SD.PartnerUser));
                    }

                    if(role==SD.PartnerUser)
                    {
                        await _userManager.AddToRoleAsync(user, SD.PartnerUser);
                    } 
                    else if(role==SD.FrontDeskUser)
                    {
                        await _userManager.AddToRoleAsync(user, SD.FrontDeskUser);
                    }
                    else if(role==SD.ManagerUser)
                    {
                        await _userManager.AddToRoleAsync(user, SD.ManagerUser);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.CustomerEndUser);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "User", new { area = "Admin" });

                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                        
                        
                    }
                //}
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
