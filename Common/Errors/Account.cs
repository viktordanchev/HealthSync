namespace Common.Errors
{
    public static class Account
    {
        public const string RequiredField = "field is required!";
        public const string InvalidEmail = "This email is invalid!";
        public const string PasswordMatch = "Confirm password field need to match password!";
        public const string UsedEmail = "This email is already registered!";
        public const string InvalidLoginData = "Invalid email or password!";
        public const string NotRegistered = "No account found with this email address!";
        public const string InvalidVrfCode = "Invalid verification code!";
    }
}
