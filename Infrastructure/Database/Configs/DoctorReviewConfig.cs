using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
{
    public class DoctorReviewConfig : IEntityTypeConfiguration<DoctorReview>
    {
        public void Configure(EntityTypeBuilder<DoctorReview> builder)
        {
            builder
                .Property(dr => dr.DateAndTime)
                .HasColumnType("timestamp without time zone");

            builder.HasData(SeedReviews());
        }

        private DoctorReview[] SeedReviews()
        {
            var ivanIvanov1 = new DoctorReview()
            {
                Id = 1,
                DoctorId = 1,
                DateAndTime = new DateTime(2024, 10, 14, 9, 0, 0),
                Rating = 5,
                Reviewer = "Aleks Petrov",
                Comment = "Dr. Ivanov went above and beyond in providing exceptional care. He took the time to listen to all my concerns, explained each step of the treatment, and made me feel at ease throughout the process. His professionalism, kindness, and dedication are truly appreciated. Highly recommended!"
            };

            var ivanIvanov2 = new DoctorReview()
            {
                Id = 2,
                DoctorId = 1,
                DateAndTime = new DateTime(2024, 9, 19, 10, 32, 0),
                Rating = 2,
                Reviewer = "Maria Kostova",
                Comment = "Some of my questions felt unanswered clearer explanations would be appreciated."
            };

            var ivanIvanov3 = new DoctorReview()
            {
                Id = 3,
                DoctorId = 1,
                DateAndTime = new DateTime(2023, 1, 24, 19, 52, 0),
                Rating = 4,
                Reviewer = "Kristin Angelova",
                Comment = "Your attention and compassion made a huge difference!"
            };

            var ivanIvanov4 = new DoctorReview()
            {
                Id = 4,
                DoctorId = 1,
                DateAndTime = new DateTime(2023, 1, 25, 13, 12, 0),
                Rating = 3,
                Reviewer = "Angel Bogdanski",
                Comment = "I’d appreciate simpler language for medical terms next time."
            };

            var ivanIvanov5 = new DoctorReview()
            {
                Id = 5,
                DoctorId = 1,
                DateAndTime = new DateTime(2024, 7, 20, 18, 52, 0),
                Rating = 5,
                Reviewer = "Kosta Adamovich",
                Comment = "Your dedication and support mean so much—thank you!"
            };

            var ivanIvanov6 = new DoctorReview()
            {
                Id = 6,
                DoctorId = 1,
                DateAndTime = new DateTime(2024, 8, 4, 7, 0, 0),
                Rating = 5,
                Reviewer = "Kristian Ivanov",
                Comment = "Your empathy and expertise are truly appreciated!"
            };

            var ivanIvanov7 = new DoctorReview()
            {
                Id = 7,
                DoctorId = 1,
                DateAndTime = new DateTime(2024, 6, 15, 22, 12, 0),
                Rating = 2,
                Reviewer = "Viktor Terziev",
                Comment = "More guidance on the next steps for my treatment would be helpful."
            };

            var mariaMarinova1 = new DoctorReview()
            {
                Id = 8,
                DoctorId = 2,
                DateAndTime = new DateTime(2024, 4, 2, 8, 2, 0),
                Rating = 5,
                Reviewer = "Yordan Angelov",
                Comment = "Thank you for your exceptional care and expertise!"
            };

            var mariaMarinova2 = new DoctorReview()
            {
                Id = 9,
                DoctorId = 2,
                DateAndTime = new DateTime(2022, 12, 12, 10, 43, 0),
                Rating = 5,
                Reviewer = "Kristian Ivanov",
                Comment = "I couldn't be more pleased with the level of care I received. Your attentiveness, kindness, and expertise made a world of difference."
            };

            var aleksKirilov1 = new DoctorReview()
            {
                Id = 10,
                DoctorId = 3,
                DateAndTime = new DateTime(2020, 7, 12, 16, 42, 0),
                Rating = 5,
                Reviewer = "Kristian Ivanov",
                Comment = "Thank you for your exceptional care and expertise!"
            };

            return [ivanIvanov1, ivanIvanov2, ivanIvanov3, ivanIvanov4, ivanIvanov5, ivanIvanov6, ivanIvanov7, mariaMarinova1, mariaMarinova2, aleksKirilov1];
        }
    }
}
