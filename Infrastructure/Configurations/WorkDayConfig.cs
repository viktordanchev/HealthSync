using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class WorkDayConfig : IEntityTypeConfiguration<WorkDay>
    {
        public void Configure(EntityTypeBuilder<WorkDay> builder)
        {
            builder.HasData(SeedWorkDays());
        }

        private WorkDay[] SeedWorkDays()
        {
            var workDay1 = new WorkDay()
            {
                Id = 1,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Monday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay2 = new WorkDay()
            {
                Id = 2,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Tuesday,
                IsWorkDay = true,
                Start = new TimeSpan(12, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay3 = new WorkDay()
            {
                Id = 3,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Wednesday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay4 = new WorkDay()
            {
                Id = 4,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Thursday,
                IsWorkDay = true,
                Start = new TimeSpan(12, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay5 = new WorkDay()
            {
                Id = 5,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Friday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay6 = new WorkDay()
            {
                Id = 6,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var workDay7 = new WorkDay()
            {
                Id = 7,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Day = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            return [workDay1, workDay2, workDay3, workDay4, workDay5, workDay6, workDay7];
        }
    }
}
