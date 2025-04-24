using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class DoctorWeekDayConfig : IEntityTypeConfiguration<DoctorWeekDay>
    {
        public void Configure(EntityTypeBuilder<DoctorWeekDay> builder)
        {
            builder.HasData(SeedWorkDays());
        }

        private DoctorWeekDay[] SeedWorkDays()
        {
            var ivanIvanov1 = new DoctorWeekDay()
            {
                Id = 1,
                DoctorId = 1,
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var ivanIvanov2 = new DoctorWeekDay()
            {
                Id = 2,
                DoctorId = 1,
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(12, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var ivanIvanov3 = new DoctorWeekDay()
            {
                Id = 3,
                DoctorId = 1,
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var ivanIvanov4 = new DoctorWeekDay()
            {
                Id = 4,
                DoctorId = 1,
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(12, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var ivanIvanov5 = new DoctorWeekDay()
            {
                Id = 5,
                DoctorId = 1,
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var ivanIvanov6 = new DoctorWeekDay()
            {
                Id = 6,
                DoctorId = 1,
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var ivanIvanov7 = new DoctorWeekDay()
            {
                Id = 7,
                DoctorId = 1,
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            var mariaMarinova1 = new DoctorWeekDay()
            {
                Id = 8,
                DoctorId = 2,
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 30),
                WorkDayEnd = new TimeOnly(16, 00),
                MeetingTimeMinutes = 15
            };

            var mariaMarinova2 = new DoctorWeekDay()
            {
                Id = 9,
                DoctorId = 2,
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 30),
                WorkDayEnd = new TimeOnly(16, 00),
                MeetingTimeMinutes = 15
            };

            var mariaMarinova3 = new DoctorWeekDay()
            {
                Id = 10,
                DoctorId = 2,
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(11, 30),
                WorkDayEnd = new TimeOnly(16, 00),
                MeetingTimeMinutes = 15
            };

            var mariaMarinova4 = new DoctorWeekDay()
            {
                Id = 11,
                DoctorId = 2,
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(11, 30),
                WorkDayEnd = new TimeOnly(16, 00),
                MeetingTimeMinutes = 15
            };

            var mariaMarinova5 = new DoctorWeekDay()
            {
                Id = 12,
                DoctorId = 2,
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(11, 30),
                WorkDayEnd = new TimeOnly(16, 00),
                MeetingTimeMinutes = 15
            };

            var mariaMarinova6 = new DoctorWeekDay()
            {
                Id = 13,
                DoctorId = 2,
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(11, 30),
                WorkDayEnd = new TimeOnly(16, 00),
                MeetingTimeMinutes = 15
            };

            var mariaMarinova7 = new DoctorWeekDay()
            {
                Id = 14,
                DoctorId = 2,
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            var aleksKirilov1 = new DoctorWeekDay()
            {
                Id = 15,
                DoctorId = 3,
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 00),
                WorkDayEnd = new TimeOnly(17, 00),
                MeetingTimeMinutes = 10
            };

            var aleksKirilov2 = new DoctorWeekDay()
            {
                Id = 16,
                DoctorId = 3,
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 00),
                WorkDayEnd = new TimeOnly(17, 00),
                MeetingTimeMinutes = 10
            };

            var aleksKirilov3 = new DoctorWeekDay()
            {
                Id = 17,
                DoctorId = 3,
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 00),
                WorkDayEnd = new TimeOnly(17, 00),
                MeetingTimeMinutes = 10
            };

            var aleksKirilov4 = new DoctorWeekDay()
            {
                Id = 18,
                DoctorId = 3,
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = false
            };

            var aleksKirilov5 = new DoctorWeekDay()
            {
                Id = 19,
                DoctorId = 3,
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = false
            };

            var aleksKirilov6 = new DoctorWeekDay()
            {
                Id = 20,
                DoctorId = 3,
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var aleksKirilov7 = new DoctorWeekDay()
            {
                Id = 21,
                DoctorId = 3,
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            var kirilConev1 = new DoctorWeekDay()
            {
                Id = 22,
                DoctorId = 4,
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(22, 00),
                MeetingTimeMinutes = 45
            };

            var kirilConev2 = new DoctorWeekDay()
            {
                Id = 23,
                DoctorId = 4,
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(22, 00),
                MeetingTimeMinutes = 45
            };

            var kirilConev3 = new DoctorWeekDay()
            {
                Id = 24,
                DoctorId = 4,
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(22, 00),
                MeetingTimeMinutes = 45
            };

            var kirilConev4 = new DoctorWeekDay()
            {
                Id = 25,
                DoctorId = 4,
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(22, 00),
                MeetingTimeMinutes = 45
            };

            var kirilConev5 = new DoctorWeekDay()
            {
                Id = 26,
                DoctorId = 4,
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(22, 00),
                MeetingTimeMinutes = 45
            };

            var kirilConev6 = new DoctorWeekDay()
            {
                Id = 27,
                DoctorId = 4,
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var kirilConev7 = new DoctorWeekDay()
            {
                Id = 28,
                DoctorId = 4,
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            var ivanaIvanova1 = new DoctorWeekDay()
            {
                Id = 29,
                DoctorId = 5,
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = false
            };

            var ivanaIvanova2 = new DoctorWeekDay()
            {
                Id = 30,
                DoctorId = 5,
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(23, 00),
                WorkDayEnd = new TimeOnly(06, 00),
                MeetingTimeMinutes = 15
            };

            var ivanaIvanova3 = new DoctorWeekDay()
            {
                Id = 31,
                DoctorId = 5,
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(23, 00),
                WorkDayEnd = new TimeOnly(06, 00),
                MeetingTimeMinutes = 15
            };

            var ivanaIvanova4 = new DoctorWeekDay()
            {
                Id = 32,
                DoctorId = 5,
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(17, 00),
                MeetingTimeMinutes = 15
            };

            var ivanaIvanova5 = new DoctorWeekDay()
            {
                Id = 33,
                DoctorId = 5,
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(17, 00),
                MeetingTimeMinutes = 15
            };

            var ivanaIvanova6 = new DoctorWeekDay()
            {
                Id = 34,
                DoctorId = 5,
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var ivanaIvanova7 = new DoctorWeekDay()
            {
                Id = 35,
                DoctorId = 5,
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            var monikaKirilova1 = new DoctorWeekDay()
            {
                Id = 36,
                DoctorId = 6,
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(20, 30),
                MeetingTimeMinutes = 60
            };

            var monikaKirilova2 = new DoctorWeekDay()
            {
                Id = 37,
                DoctorId = 6,
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(20, 30),
                MeetingTimeMinutes = 60
            };

            var monikaKirilova3 = new DoctorWeekDay()
            {
                Id = 38,
                DoctorId = 6,
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(10, 00),
                WorkDayEnd = new TimeOnly(20, 30),
                MeetingTimeMinutes = 60
            };

            var monikaKirilova4 = new DoctorWeekDay()
            {
                Id = 39,
                DoctorId = 6,
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(12, 00),
                WorkDayEnd = new TimeOnly(20, 30),
                MeetingTimeMinutes = 60
            };

            var monikaKirilova5 = new DoctorWeekDay()
            {
                Id = 40,
                DoctorId = 6,
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(12, 00),
                WorkDayEnd = new TimeOnly(20, 30),
                MeetingTimeMinutes = 60
            };

            var monikaKirilova6 = new DoctorWeekDay()
            {
                Id = 41,
                DoctorId = 6,
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var monikaKirilova7 = new DoctorWeekDay()
            {
                Id = 42,
                DoctorId = 6,
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            return [ivanIvanov1, ivanIvanov2, ivanIvanov3, ivanIvanov4, ivanIvanov5,
                    ivanIvanov6, ivanIvanov7, mariaMarinova1, mariaMarinova2, mariaMarinova3,
                    mariaMarinova4, mariaMarinova5, mariaMarinova6, mariaMarinova7, aleksKirilov1,
                    aleksKirilov2, aleksKirilov3, aleksKirilov4, aleksKirilov5, aleksKirilov6,
                    aleksKirilov7, kirilConev1, kirilConev2, kirilConev3, kirilConev4,
                    kirilConev5, kirilConev6, kirilConev7, ivanaIvanova1, ivanaIvanova2,
                    ivanaIvanova3, ivanaIvanova4, ivanaIvanova5, ivanaIvanova6, ivanaIvanova7,
                    monikaKirilova1, monikaKirilova2, monikaKirilova3, monikaKirilova4, monikaKirilova5,
                    monikaKirilova6, monikaKirilova7];
        }
    }
}
