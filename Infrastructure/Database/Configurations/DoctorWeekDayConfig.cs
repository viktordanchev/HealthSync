﻿using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
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
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay2 = new DoctorWeekDay()
            {
                Id = 2,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Tuesday,
                IsWorkDay = true,
                Start = new TimeSpan(12, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay3 = new DoctorWeekDay()
            {
                Id = 3,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Wednesday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay4 = new DoctorWeekDay()
            {
                Id = 4,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Thursday,
                IsWorkDay = true,
                Start = new TimeSpan(12, 30, 0),
                End = new TimeSpan(17, 30, 0),
                MeetingTimeMinutes = 30
            };

            var workDay5 = new DoctorWeekDay()
            {
                Id = 5,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                WeekDay = DayOfWeek.Friday,
                IsWorkDay = true,
                Start = new TimeSpan(9, 30, 0),
                End = new TimeSpan(17, 30, 0),
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
