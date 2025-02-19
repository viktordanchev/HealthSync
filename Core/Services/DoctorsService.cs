using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;
using Core.Interfaces.ExternalServices;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

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

        public async Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userEmail)
        {
            var doctors = await _repository.GetDoctorsAsync(requestData, userEmail);

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
            var doctor = await _repository.GetDoctorDetailsAsync(doctorId);

            return doctor;
        }

        public async Task<bool> IsDoctorExistAsync(int doctorId)
        {
            return await _repository.IsDoctorExistAsync(doctorId);
        }

        public async Task AddDoctorAsync(BecomeDoctorRequest requestData, string userId)
        {
            var imgUrl = await _GCSService.UploadProfileImageAsync(requestData.ProfilePhoto);

            var doctorId = await _repository.AddDoctorAsync(requestData, userId, imgUrl);

            await _repository.GenerateEmptyDoctorWeekSchedule(doctorId);
        }

        public async Task<bool> IsUserDoctorAsync(string userId)
        {
            var isUserDoctor = await _repository.IsUserDoctorAsync(userId);

            return isUserDoctor;
        }

        public async Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId)
        {
            var doctorInfo = await _repository.GetDoctorPersonalInfoAsync(userId);

            return doctorInfo;
        }
    }
}
