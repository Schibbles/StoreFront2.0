using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is Required*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Subject is Required&")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Must provide a Message Body*")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}