﻿namespace Common
{
    public static class Validations
    {
        public static class User 
        {
            public const string NameMatch = @"^[A-Z][a-z]+$";
            public const int UNCLength = 10;
            public const string UNCMatch = @"^\d+$";
        }
    }
}
