using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShoesLover.Models
{
    public class User
    {
        private string Fullname;
        private string Email;
        private string Password;
        [Required(ErrorMessage ="Please insert fullname")]
        public string fullname
        {
            get { return Fullname; }
            set { Fullname = value; }
        }
        [Required(ErrorMessage = "Please insert email")]
        public string email
        {
            get { return Email; }
            set { Email = value; }
        }
        [Required(ErrorMessage = "Please insert password")]
        public string password
        {
            get { return Password; }
            set { Password = value; }
        }
        public User()
        {

        }
        public User(string FullName, string EMail, string PAssword)
        {
            this.Fullname = FullName;
            this.Email = EMail;
            this.Password = PAssword;
        }
    }
}
