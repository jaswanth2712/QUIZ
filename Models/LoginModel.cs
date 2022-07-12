using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QUIZ.Models
{
    public class LoginModel
    {
        public int ID { get; set; }
        [RegularExpression("[A-Za-z][A-Za-z0-9]+",ErrorMessage ="Invalid Name")]
        public string UserName { get; set; }        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
