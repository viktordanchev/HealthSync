using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configs
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
                ImgUrl = "https://healthsyncstorage.blob.core.windows.net/profile-images/a3717562-385e-41ce-9eff-0f1b994e5548.jpg",
                Information = "I am Dr. Ivan Ivanov, an orthodontist with over 10 years of experience. I earned my Doctor of Dental Medicine (DMD) degree from Sofia Medical University, where I also completed my orthodontic specialization. I have worked in various reputable dental clinics, providing treatments such as braces, clear aligners, and other advanced orthodontic procedures for patients of all ages. I focus on delivering personalized care, creating treatment plans tailored to each patient’s specific needs. I hold certifications in advanced orthodontic techniques and regularly attend courses to stay updated with the latest advancements in the field. My goal is to ensure that every patient receives the best possible outcome. Known for my compassionate approach and attention to detail, I strive to help my patients achieve healthier, more beautiful smiles. My dedication to patient satisfaction and passion for orthodontics have earned me a solid reputation in the field, making me a trusted choice for care."
            };

            var doctor2 = new Doctor()
            {
                Id = 2,
                IdentityId = "4d650e24-6b66-41e3-8391-efab8c31a1dd", //Maria Marinova, m.marinova@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 2, //Endocrinologist
                ImgUrl = "https://healthsyncstorage.blob.core.windows.net/profile-images/4d650e24-6b66-41e3-8391-efab8c31a1dd.jpg",
                Information = "I graduated from the Medical University of Sofia and have been practicing medicine for over 12 years. Throughout my career, I have focused on internal medicine, always striving to offer thorough and personalized care. I believe in building strong relationships with my patients to ensure long-term health and well-being."
            };

            var doctor3 = new Doctor()
            {
                Id = 3,
                IdentityId = "88cd5a7b-01d8-49b4-8688-35cd23751532", //Aleks Kirilov, a.kirilov@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 2, //Endocrinologist
                ImgUrl = "https://healthsyncstorage.blob.core.windows.net/profile-images/88cd5a7b-01d8-49b4-8688-35cd23751532.jpg",
                Information = "I am a dedicated endocrinologist with a passion for helping patients manage their hormonal health. I graduated from Sofia Medical University and completed my residency in endocrinology at the same institution. With over 8 years of experience, I specialize in treating conditions such as diabetes, thyroid disorders, and adrenal gland issues. My approach to patient care is holistic, focusing on both medical treatment and lifestyle modifications to achieve optimal health outcomes. I am committed to staying updated with the latest advancements in endocrinology to provide the best care possible."
            };

            var doctor4 = new Doctor()
            {
                Id = 4,
                IdentityId = "95189f02-fb1a-4700-95e3-6146b8aa8b15", //Kiril Conev, k.conev@mail.com
                HospitalId = 1, //Sunnybrook General Hospital
                SpecialtyId = 3, //Cardiologist
                ImgUrl = "https://healthsyncstorage.blob.core.windows.net/profile-images/95189f02-fb1a-4700-95e3-6146b8aa8b15.jpg",
                Information = "I am a cardiologist with a strong commitment to patient care and education. I graduated from Sofia Medical University and completed my residency in cardiology at the same institution. With over 10 years of experience, I specialize in diagnosing and treating various heart conditions, including hypertension, coronary artery disease, and heart failure. My approach to patient care emphasizes prevention and lifestyle modifications, and I work closely with my patients to develop personalized treatment plans. I am dedicated to staying current with the latest advancements in cardiology to provide the best possible care."
            };

            var doctor5 = new Doctor()
            {
                Id = 5,
                IdentityId = "f37b43ca-86a2-4b11-972d-5e0569f4deb3", //Ivana Ivanova, i.ivanova@mail.com
                HospitalId = 2, //Pine Hills Medical Center
                SpecialtyId = 4, //Neurologist
                ImgUrl = "https://healthsyncstorage.blob.core.windows.net/profile-images/f37b43ca-86a2-4b11-972d-5e0569f4deb3.jpg",
                Information = "I am a neurologist with a passion for understanding the complexities of the human brain and nervous system. I graduated from Sofia Medical University and completed my residency in neurology at the same institution. With over 7 years of experience, I specialize in diagnosing and treating neurological disorders such as epilepsy, multiple sclerosis, and migraines. My approach to patient care is comprehensive, focusing on both medical treatment and lifestyle modifications to improve overall health. I am committed to providing compassionate care and staying updated with the latest advancements in neurology."
            };

            var doctor6 = new Doctor()
            {
                Id = 6,
                IdentityId = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", //Monika Kirilova, m.kirilova@mail.com
                HospitalId = 2, //Pine Hills Medical Center
                SpecialtyId = 3, //Cardiologist
                ImgUrl = "https://healthsyncstorage.blob.core.windows.net/profile-images/d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35.jpg",
                Information = "I am a cardiologist with a strong commitment to patient care and education. I graduated from Sofia Medical University and completed my residency in cardiology at the same institution. With over 10 years of experience, I specialize in diagnosing and treating various heart conditions, including hypertension, coronary artery disease, and heart failure. My approach to patient care emphasizes prevention and lifestyle modifications, and I work closely with my patients to develop personalized treatment plans. I am dedicated to staying current with the latest advancements in cardiology to provide the best possible care."
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
