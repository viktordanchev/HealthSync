using System.Threading.Channels;

namespace Common
{
    public static class Messages
    {
        public static class Account
        {
            public const string NewVrfCode = "Verification code is sended";
            public const string SendedPassRecoverLink = "Recover password link was sended to your email";
            public const string UserDataUpdated = "Updated successfully";
        }

        public static class Meetings
        {
            public const string AddedMeeting = "Meeting was added successfully";
            public const string DeletedMeeting = "Meeting was deleted successfully";
        }

        public static class Reviews
        {
            public const string AddedReview = "Rating was added successfully";
        }

        public static class Doctors
        {
            public const string RegisteredDoctor = "You are doctor now";
            public const string UpdatedDaysOff = "Days off updated successfully";
            public const string UpdatedWeeklySchedule = "Weekly schedule updated successfully";
            public const string UpdatedProfileInfo = "Some changes may take a moment to update";
        }
    }
}
