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
                Id = "44a35b22-2cdb-44bd-8286-d7ec7eaa2248",
                IdenitityId = "a3717562-385e-41ce-9eff-0f1b994e5548", //Ivan Ivanov, i.ivanov@mail.com
                HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872", //Sunnybrook General Hospital
                SpecialtyId = "d50a6c34-f6d3-4ff8-b1ad-299dcb776789", //Orthodontist
                ImgUrl = "https://storage.cloud.google.com/healthsync/ivan-ivanov.jpg"
            };

            var doctor2 = new Doctor()
            {
                Id = "43eb5263-a106-4d5b-909f-92294b21f360",
                IdenitityId = "4d650e24-6b66-41e3-8391-efab8c31a1dd", //Maria Marinova, m.marinova@mail.com
                HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872", //Sunnybrook General Hospital
                SpecialtyId = "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d", //Endocrinologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/maria-marinova.jpg"
            };

            var doctor3 = new Doctor()
            {
                Id = "4411897e-f897-404e-95bd-85e683b34ff5",
                IdenitityId = "88cd5a7b-01d8-49b4-8688-35cd23751532", //Aleks Kirilov, a.kirilov@mail.com
                HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872", //Sunnybrook General Hospital
                SpecialtyId = "c3a9d3e1-5b4b-4ae1-b96c-8ef6bdb17c0d", //Endocrinologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/aleks-kirilov.jpg"
            };

            var doctor4 = new Doctor()
            {
                Id = "97e6c00a-664f-4861-8976-4058aed9cdb0",
                IdenitityId = "95189f02-fb1a-4700-95e3-6146b8aa8b15", //Kiril Conev, k.conev@mail.com
                HospitalId = "505d9322-7cca-4bca-ae59-683ff3089872", //Sunnybrook General Hospital
                SpecialtyId = "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74", //Cardiologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/kiril-conev.jpg"
            };

            var doctor5 = new Doctor()
            {
                Id = "702abf92-a237-460a-94b5-5763127fb627",
                IdenitityId = "f37b43ca-86a2-4b11-972d-5e0569f4deb3", //Ivana Ivanova, i.ivanova@mail.com
                HospitalId = "710649bb-deb0-4271-a97f-6e5cde3d2fe6", //Pine Hills Medical Center
                SpecialtyId = "01b2c6e4-cf32-4b44-84ae-4e4e2c17d8f9", //Neurologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/ivana-ivanova.jpg"
            };

            var doctor6 = new Doctor()
            {
                Id = "77bb4f69-7cb7-4a8d-acb2-5464b74cfff1",
                IdenitityId = "d99b0dbf-6a91-4dc0-a29e-9ffd46f79d35", //Monika Kirilova, m.kirilova@mail.com
                HospitalId = "710649bb-deb0-4271-a97f-6e5cde3d2fe6", //Pine Hills Medical Center
                SpecialtyId = "f24f8a2e-2f91-4be1-85a9-f1e6b4f82b74", //Cardiologist
                ImgUrl = "https://storage.cloud.google.com/healthsync/monika-kirilova.jpg"
            };

            var doctor7 = new Doctor()
            {
                Id = "6ba6d91c-3c17-4a90-a73b-87085a17861a",
                IdenitityId = "78850da7-a0ff-42f3-a862-d162457910a0", //Vanya Yankova, v.yankova@mail.com
                HospitalId = "710649bb-deb0-4271-a97f-6e5cde3d2fe6", //Pine Hills Medical Center
                SpecialtyId = "d50a6c34-f6d3-4ff8-b1ad-299dcb776789", //Orthodontist
            };

            return [doctor1, doctor2, doctor3, doctor4, doctor5, doctor6, doctor7];
        }
    }
}
