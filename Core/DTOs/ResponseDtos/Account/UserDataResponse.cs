﻿namespace Core.DTOs.ResponseDtos.Account
{
    public class UserDataResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
