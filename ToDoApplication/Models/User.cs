using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoApplication.Models
{
    public class User
    {
        [Key]
        [RegularExpression("^[A-Z]{3,3}[0-9]{4,4}$")]
        public string UserCode { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}