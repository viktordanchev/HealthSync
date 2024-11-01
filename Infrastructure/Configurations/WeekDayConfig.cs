using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class WeekDayConfig : IEntityTypeConfiguration<WeekDay>
    {
        public void Configure(EntityTypeBuilder<WeekDay> builder)
        {
            builder.HasData(SeedWorkDays());
        }

        private WeekDay[] SeedWorkDays()
        {
            var workDay1 = new WeekDay()
            {
                Id = 1,
                DoctorId = 1,
                Day = DayOfWeek.Monday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0)
            };

            var workDay2 = new WeekDay()
            {
                Id = 2,
                DoctorId = 1,
                Day = DayOfWeek.Tuesday,
                IsWorkDay = true,
                Start = new TimeSpan(12, 30, 0),
                End = new TimeSpan(17, 30, 0)
            };

            var workDay3 = new WeekDay()
            {
                Id = 3,
                DoctorId = 1,
                Day = DayOfWeek.Wednesday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0)
            };

            var workDay4 = new WeekDay()
            {
                Id = 4,
                DoctorId = 1,
                Day = DayOfWeek.Thursday,
                IsWorkDay = true,
                Start = new TimeSpan(12, 30, 0),
                End = new TimeSpan(17, 30, 0)
            };

            var workDay5 = new WeekDay()
            {
                Id = 5,
                DoctorId = 1,
                Day = DayOfWeek.Friday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0)
            };

            var workDay6 = new WeekDay()
            {
                Id = 6,
                DoctorId = 1,
                Day = DayOfWeek.Saturday,
                IsWorkDay = false
            };

            var workDay7 = new WeekDay()
            {
                Id = 7,
                DoctorId = 1,
                Day = DayOfWeek.Sunday,
                IsWorkDay = false
            };

            return [workDay1, workDay2, workDay3, workDay4, workDay5, workDay6, workDay7];
        }
    }
}
