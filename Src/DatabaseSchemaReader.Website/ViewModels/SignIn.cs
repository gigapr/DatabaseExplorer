﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseSchemaReader.Website.ViewModels
{
    public class SignIn
    {
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your email")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public String Password { get; set; }        
    }
}