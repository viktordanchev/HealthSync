using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(SeedReviews());
        }

        private Review[] SeedReviews()
        {
            var review1 = new Review()
            {
                Id = 1,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2024, 10, 14, 9, 0, 0),
                Rating = 5,
                Reviewer = "Aleks Petrov"
            };

            var review2 = new Review()
            {
                Id = 2,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2024, 9, 19, 10, 32, 0),
                Rating = 2,
                Reviewer = "Maria Kostova"
            };

            var review3 = new Review()
            {
                Id = 3,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2023, 1, 24, 19, 52, 0),
                Rating = 4,
                Reviewer = "Kristin Angelova"
            };

            var review4 = new Review()
            {
                Id = 4,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2023, 1, 25, 13, 12, 0),
                Rating = 3,
                Reviewer = "Angel Bogdanski"
            };

            var review5 = new Review()
            {
                Id = 5,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2024, 7, 20, 18, 52, 0),
                Rating = 6,
                Reviewer = "Kosta Adamovich"
            };

            var review6 = new Review()
            {
                Id = 6,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2024, 8, 4, 7, 0, 0),
                Rating = 6,
                Reviewer = "Kristian Ivanov"
            };

            var review7 = new Review()
            {
                Id = 7,
                DoctorId = 1, //Ivan Ivanov, i.ivanov@mail.com
                Date = new DateTime(2024, 6, 15, 22, 12, 0),
                Rating = 2,
                Reviewer = "Viktor Terziev"
            };

            var review8 = new Review()
            {
                Id = 8,
                DoctorId = 2, //Maria Marinova, m.marinova@mail.com
                Date = new DateTime(2024, 4, 2, 8, 2, 0),
                Rating = 5,
                Reviewer = "Yordan Angelov"
            };

            var review9 = new Review()
            {
                Id = 9,
                DoctorId = 2, //Maria Marinova, m.marinova@mail.com
                Date = new DateTime(2022, 12, 12, 10, 43, 0),
                Rating = 5,
                Reviewer = "Kristian Ivanov"
            };

            return [review1, review2, review3, review4, review5, review6, review7, review8, review9];
        }
    }
}
