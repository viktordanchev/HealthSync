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
            var workDay1 = new DoctorWeekDay()
            {
                Id = 1,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Monday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var workDay2 = new DoctorWeekDay()
            {
                Id = 2,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(12, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var workDay3 = new DoctorWeekDay()
            {
                Id = 3,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var workDay4 = new DoctorWeekDay()
            {
                Id = 4,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(12, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var workDay5 = new DoctorWeekDay()
            {
                Id = 5,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                WorkDayStart = new TimeOnly(9, 30),
                WorkDayEnd = new TimeOnly(17, 30),
                MeetingTimeMinutes = 30
            };

            var workDay6 = new DoctorWeekDay()
            {
                Id = 6,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var workDay7 = new DoctorWeekDay()
            {
                Id = 7,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            return [workDay1, workDay2, workDay3, workDay4, workDay5, workDay6, workDay7];
        }
    }
}
