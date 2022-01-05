using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        private string _returnUrl;
        public string ReturnUrl
        {
            get
            {
                return string.IsNullOrEmpty(_returnUrl) ? "/Home" : _returnUrl;
            }
            set
            {
                _returnUrl = value;
            }
        }
    }
}
