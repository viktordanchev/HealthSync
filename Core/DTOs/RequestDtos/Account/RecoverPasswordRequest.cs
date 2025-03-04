﻿using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RequestDtos.Account
{
    public class RecoverPasswordRequest
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
