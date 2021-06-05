using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoftPrimes.Shared.ViewModels
{
    public class UserModel
    {
        public bool IsAuthenticated { get; set; }
    }
    public class UserLoginModel
    {
        [Required]
        [MaxLength(450)]
        public string Username { get; set; }
        [Required]
        [MaxLength(450)]
        public string Password { get; set; }
        public string applicationType { get; set; } = "1";
        public bool Continue { get; set; } = false;
        public bool DisableSSO { get; set; }
    }
    public class LoginModel
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Lat { get; set; }
        public string longs { get; set; }
        public string MacId { get; set; }
        public string MobileType { get; set; }
        public string Network { get; set; }
        public string SerailNumber { get; set; }
    }
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "FullNameAr")]
        public string FullNameAr { get; set; }
        [Required]
        [Display(Name = "FullNameEn")]
        public string FullNameEn { get; set; }
        public int AgentType { get; set; }
        public bool Active { get; set; }
        public string SupervisorId { get; set; }
        public int CompanyId { get; set; }
        public byte[] Image { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
    }
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
