using Core.Interfaces.ExternalServices;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Models.ResponseDtos.Doctors;
using RestAPI.Dtos.RequestDtos.Doctors;

namespace Core.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _repository;
        private readonly IGoogleCloudStorageService _GCSService;

        public DoctorsService(IDoctorsRepository repository, IGoogleCloudStorageService gcsService)
        {
            _repository = repository;
            _GCSService = gcsService;
        }

        public async Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userIdentityId)
        {
            var doctors = await _repository.GetDoctorsAsync(requestData, userIdentityId);

            switch (requestData.Sorting.ToString())
            {
                case "NameAsc":
                    doctors = doctors.OrderBy(d => d.Name).ToList();
                    break;
                case "NameDesc":
                    doctors = doctors.OrderByDescending(d => d.Name).ToList();
                    break;
                case "RatingAsc":
                    doctors = doctors.OrderBy(d => d.Rating).ToList();
                    break;
                case "RatingDesc":
                    doctors = doctors.OrderByDescending(d => d.Rating).ToList();
                    break;
            }

            return doctors;
        }

        public async Task<DoctorDetailsResponse> GetDoctorDetailsAsync(int doctorId)
        {
            var doctor = await _context.Doctors
                .AsNoTracking()
                .Where(d => d.Id == doctorId)
                .Select(d => new DoctorDetailsResponse()
                {
                    Id = d.Id,
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    Specialty = d.Specialty.Type,
                    Rating = d.Reviews.Any() ? Math.Round(d.Reviews.Average(r => r.Rating), 1) : 0,
                    TotalReviews = d.Reviews.Where(r => r.DoctorId == d.Id).Count(),
                    HospitalName = d.Hospital.Name,
                    HospitalAddress = d.Hospital.Address,
                    Information = d.Information,
                    ContactEmail = d.ContactEmail,
                    ContactPhoneNumber = d.ContactPhoneNumber
                })
                .FirstOrDefaultAsync();

            return doctor;
        }

        public async Task<bool> IsDoctorExistAsync(int doctorId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            return doctor != null;
        }

        public async Task AddDoctorAsync(BecomeDoctorRequest requestData, string userIdentityId)
        {
            string? imgUrl = null;

            if (requestData.ProfilePhoto != null)
            {
                imgUrl = await _GCSService.UploadProfileImageAsync(requestData.ProfilePhoto);
            }

            var doctor = new Doctor()
            {
                IdentityId = userIdentityId,
                HospitalId = requestData.HospitalId,
                SpecialtyId = requestData.SpecialtyId,
                ContactEmail = requestData.ContactEmail,
                ContactPhoneNumber = requestData.ContactPhoneNumber,
                ImgUrl = imgUrl
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            await GenerateEmptyDoctorWeekSchedule(doctor.Id);
        }

        public async Task<bool> IsUserDoctorAsync(string userId)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.IdentityId == userId);

            return doctor != null;
        }

        public async Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId)
        {
            var doctorInfo = await _context.Doctors
                .Where(d => d.IdentityId == userId)
                .Select(d => new DoctorPersonalInfoResponse()
                {
                    Name = $"{d.Identity.FirstName} {d.Identity.LastName}",
                    ImgUrl = d.ImgUrl,
                    HospitalId = d.HospitalId,
                    Hospital = d.Hospital.Name,
                    SpecialtyId = d.SpecialtyId,
                    Specialty = d.Specialty.Type,
                    PersonalInformation = d.Information,
                    ContactEmail = d.ContactEmail,
                    ContactPhoneNumber = d.ContactPhoneNumber,
                    WeeklySchedule = d.WorkWeek
                        .Select(wd => new WeekDayResponse()
                        {
                            Id = wd.Id,
                            WeekDay = wd.WeekDay,
                            IsWorkDay = wd.IsWorkDay,
                            WorkDayStart = wd.Start,
                            WorkDayEnd = wd.End,
                            MeetingTimeMinutes = wd.MeetingTimeMinutes
                        })
                        .ToList()
                })
                .FirstAsync();

            return doctorInfo;
        }

        private async Task GenerateEmptyDoctorWeekSchedule(int doctorId)
        {
            var week = new List<DoctorWeekDay>();

            for (int day = 0; day < 7; day++)
            {
                var workDay = new DoctorWeekDay()
                {
                    DoctorId = doctorId,
                    WeekDay = (DayOfWeek)((day + 1) % 7),
                    IsWorkDay = false
                };
            }

            await _context.DoctorWeekDays.AddRangeAsync(week);
            await _context.SaveChangesAsync();
        }
    }
}
