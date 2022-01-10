using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ShoesLover.Models
{
    public class User
    {
        public int ID { get; set; }
        private string _fullname;
        private string _email;
        private string _password;
        private string _phone;
        [Required(ErrorMessage ="Please insert fullname")]
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }
        [Required(ErrorMessage = "Please insert email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        [Required(ErrorMessage = "Please insert password")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        [Required(ErrorMessage = "Please insert phone number")]
        public string Phone
        {
            get { 
                if (string.IsNullOrEmpty(_phone)) 
                    return _password;
                else return "";
            }
            set { _password = value; }
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
