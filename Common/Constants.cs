namespace Common
{
    public static class Constants
    {
        public const int MonthRangeMin = 1;
        public const int MonthRangeMax = 12;

        public static class User
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }

        public static class Reviews
        {
            public const int RatingRangeMin = 1;
            public const int RatingRangeMax = 5;
            public const int CommentMaxLength = 300;
        }

        public static class Doctors
        {
            public const int InformationMaxLength = 1000;
            public const int MeetingTimeMinutesMin = 5;
            public const int MeetingTimeMinutesMax = 60;
        }
    }
}
