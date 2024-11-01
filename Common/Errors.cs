namespace Common
{
    public static class Errors
    {
        public const string RequiredField = "field is required!";

        public static class Account
        {
            public const string InvalidEmail = "This email is invalid!";
            public const string PasswordMatch = "Confirm password field need to match password!";
            public const string UsedEmail = "This email is already registered!";
            public const string InvalidLoginData = "Invalid email or password!";
            public const string NotRegistered = "No account found with this email address!";
            public const string InvalidVrfCode = "Invalid verification code!";
            public const string AlredyVerified = "This email already is verified!";
            public const string InvalidToken = "This token is invalid!";
        }

        public static class Doctors
        {
            public const string InvalidRating = "Rating must be between 1 and 5!";
            public const string InvalidDoctorId = "This doctor id is invalid!";
            public const string ItsDayOff = "Sorry, but today is a holiday!";
            public const string InvalidDate = "Date must be greater than today!";
        }
    }
}
