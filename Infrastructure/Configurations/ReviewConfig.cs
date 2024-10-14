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
                Id = "ba9d730a-fcf5-4662-adfd-23bfa49e10f1",
                DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", //Ivan Ivanov, i.ivanov@mail.com
                Text = "An amazing doctor!",
                Date = new DateTime(2024, 10, 14, 9, 0, 0),
                Rating = 5,
                Reviewer = "Aleks Petrov"
            };

            var review2 = new Review()
            {
                Id = "4d3e3800-b989-4406-85cc-f0ecd1080b88",
                DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", //Ivan Ivanov, i.ivanov@mail.com
                Text = "Not impressed with the service.",
                Date = new DateTime(2024, 9, 19, 10, 32, 0),
                Rating = 2,
                Reviewer = "Maria Kostova"
            };

            var review3 = new Review()
            {
                Id = "1f4ac07c-673a-4936-b0e4-196dd7488192",
                DoctorId = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248", //Ivan Ivanov, i.ivanov@mail.com
                Text = "A wonderful experience overall!",
                Date = new DateTime(2023, 1, 24, 19, 52, 0),
                Rating = 4,
                Reviewer = "Kristin Angelova"
            };

            var review4 = new Review()
            {
                Id = "c6f58c38-5d0c-4006-8697-ac72ee11fe29",
                DoctorId = "43eb5263-a106-4d5b-909f-92294b21f360", //Maria Marinova, m.marinova@mail.com
                Text = "Amazing!",
                Date = new DateTime(2024, 4, 2, 8, 2, 0),
                Rating = 5,
                Reviewer = "Yordan Angelov"
            };

            var review5 = new Review()
            {
                Id = "b2089e63-a041-4638-b308-c3ab8b6551ea",
                DoctorId = "43eb5263-a106-4d5b-909f-92294b21f360", //Maria Marinova, m.marinova@mail.com
                Text = "Best doctor.",
                Date = new DateTime(2022, 12, 12, 10, 43, 0),
                Rating = 5,
                Reviewer = "Kristian Ivanov"
            };

            return [review1, review2, review3, review4, review5];
        }
    }
}
