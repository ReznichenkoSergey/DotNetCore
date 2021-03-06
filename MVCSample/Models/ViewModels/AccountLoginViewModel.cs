﻿using System.ComponentModel.DataAnnotations;

namespace MVCSample.Models.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool SavePassword { get; set; }
    }
}
