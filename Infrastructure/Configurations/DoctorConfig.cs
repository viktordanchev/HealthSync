using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DoctorConfig : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasData(SeedDoctors());
        }

        private Doctor[] SeedDoctors()
        {
            var doctor1 = new Doctor()
            {
                Id = 1,
                IdentityId = "a3717562-385e-41ce-9eff-0f1b994e5548", //Ivan Ivanov, i.ivanov@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 1, //Orthodontist
                ImgUrl = "https://storage.cloud.google.com/healthsync/profile-images/Ivan-Ivanov.jpg",
                Information = "I am Dr. Ivan Ivanov, an orthodontist with over 10 years of experience. I earned my Doctor of Dental Medicine (DMD) degree from Sofia Medical University, where I also completed my orthodontic specialization. I have worked in various reputable dental clinics, providing treatments such as braces, clear aligners, and other advanced orthodontic procedures for patients of all ages. I focus on delivering personalized care, creating treatment plans tailored to each patient’s specific needs. I hold certifications in advanced orthodontic techniques and regularly attend courses to stay updated with the latest advancements in the field. My goal is to ensure that every patient receives the best possible outcome. Known for my compassionate approach and attention to detail, I strive to help my patients achieve healthier, more beautiful smiles. My dedication to patient satisfaction and passion for orthodontics have earned me a solid reputation in the field, making me a trusted choice for care."
            };

            var doctor2 = new Doctor()
            {
                Id = 2,
                IdentityId = "4d650e24-6b66-41e3-8391-efab8c31a1dd", //Maria Marinova, m.marinova@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 2, //Endocrinologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/profile-images/Maria-Marinova.jpg"
            };

            var doctor3 = new Doctor()
            {
                Id = 3,
                IdentityId = "88cd5a7b-01d8-49b4-8688-35cd23751532", //Aleks Kirilov, a.kirilov@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 2, //Endocrinologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/profile-images/Aleks-Kirilov.jpg"
            };

            var doctor4 = new Doctor()
            {
                Id = 4,
                IdentityId = "95189f02-fb1a-4700-95e3-6146b8aa8b15", //Kiril Conev, k.conev@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 3, //Cardiologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/profile-images/Kiril-Conev.jpg"
            };

            var doctor5 = new Doctor()
            {
                Id = 5,
                IdentityId = "f37b43ca-86a2-4b11-972d-5e0569f4deb3", //Ivana Ivanova, i.ivanova@mail.com
                HospitalId = 2, //Pine Hills Medical Center
                SpecialtyId = 4, //Neurologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/profile-images/Ivana-Ivanova.jpg"
            };

            var doctor6 = new Doctor()
            {
                Id = 6,
                IdentityId = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", //Monika Kirilova, m.kirilova@mail.com
                HospitalId = 2, //Pine Hills Medical Center
                SpecialtyId = 3, //Cardiologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/profile-images/Monika-Kirilova.jpg"
            };

            var doctor7 = new Doctor()
            {
                Id = 7,
                IdentityId = "78850da7-a0ff-42f3-a862-d162457910a0", //Vanya Yankova, v.yankova@mail.com
                HospitalId = 2, //Pine Hills Medical Center
                SpecialtyId = 1, //Orthodontist
            };

            return [doctor1, doctor2, doctor3, doctor4, doctor5, doctor6, doctor7];
        }
    }
}
