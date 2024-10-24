﻿using System.ComponentModel.DataAnnotations;
using static Common.Errors;

namespace RestAPI.RequestDtos.Account
{
    public class LoginRequest
    {
        [Required(ErrorMessage = $"Email {RequiredField}")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = $"Password {RequiredField}")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
