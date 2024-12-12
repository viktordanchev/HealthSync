namespace Common
{
    public static class Errors
    {
        public const string RequiredField = "field is required!";
        public const string InvalidRequest = "Something went wrong!";
        public const string SessionEnd = "Something went wrong!";

        public static class Account
        {
            public const string InvalidEmail = "This email is invalid!";
            public const string InvalidCurrentPassword = "Wrong current password!";
            public const string PasswordMatch = "Confirm password field need to match password!";
            public const string SamePassword = "New password cannot match the current password!";
            public const string UsedEmail = "This email is already registered!";
            public const string InvalidLoginData = "Invalid email or password!";
            public const string NotRegistered = "No account found with this email address!";
            public const string InvalidVrfCode = "Invalid verification code!";
            public const string InvalidToken = "This token is invalid!";
        }

        public static class Meetings
        {
            public const string ExistingMeeting = "You’ve already booked a meeting with this doctor!";
        }
    }
}
