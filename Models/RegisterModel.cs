using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QUIZ.Models;

namespace QUIZ.Models
{
    public class RegisterModel
    {
       
        [Required]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9]+",ErrorMessage ="Invalid Name")]
       // [UserNameValidate(ErrorMessage ="UserName Already Exists")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public  string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        
       // [PasswordValidate("Password")]
       [Compare("Password",ErrorMessage ="Password must be same")]
        public string confirmPassword { get; set; }
        public string Role { get; set; }

    }
   
}

