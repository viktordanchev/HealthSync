using Core.Constants;
using Core.DTOs.RequestDtos.Doctors;
using Core.DTOs.ResponseDtos.Doctors;
using Core.DTOs.ResponseDtos.Specialties;
using Core.Interfaces.ExternalServices;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;

namespace Core.Services
{
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly ISpecialtiesRepository _specialtiesRepo;
        private readonly IBlobStorageServiceService _BlobStorageService;

        public DoctorsService(IDoctorsRepository repository,
            ISpecialtiesRepository specialtiesRepo,
            IBlobStorageServiceService blobStorageService)
        {
            _doctorsRepository = repository;
            _specialtiesRepo = specialtiesRepo;
            _BlobStorageService = blobStorageService;
        }

        public async Task<IEnumerable<DoctorResponse>> GetDoctorsAsync(GetDoctorsRequest requestData, string userEmail)
        {
            var doctors = await _doctorsRepository.GetDoctorsAsync(requestData, userEmail);
            doctors.ToList().ForEach(d => d.Rating = Math.Round(d.Rating, 1));

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
            var doctor = await _doctorsRepository.GetDoctorDetailsAsync(doctorId);
            doctor.Rating = Math.Round(doctor.Rating, 1);

            return doctor;
        }

        public async Task AddDoctorAsync(ProfileInfoRequest requestData, string userId)
        {
            var imgUrl = await _BlobStorageService.UploadProfileImageAsync(requestData.ProfilePhoto, BlobStorageContainers.ProfileImages, userId);

            var doctorId = await _doctorsRepository.AddDoctorAsync(requestData, userId, imgUrl);

            await _doctorsRepository.GenerateEmptyDoctorWeekSchedule(doctorId);
        }

        public async Task<DoctorPersonalInfoResponse> GetDoctorPersonalInfoAsync(string userId)
        {
            var doctorInfo = await _doctorsRepository.GetDoctorPersonalInfoAsync(userId);

            return doctorInfo;
        }

        public async Task<IEnumerable<SpecialtyResponse>> GetSpecialtiesAsync()
        {
            var specialties = await _specialtiesRepo.GetSpecialtiesAsync();

            return specialties;
        }

        public async Task<IEnumerable<DoctorResponse>> GetTopDoctorsAsync()
        {
            var doctors = await _doctorsRepository.GetTopDoctorsAsync();
            doctors.ToList().ForEach(d => d.Rating = Math.Round(d.Rating, 1));

            return doctors;
        }

        public async Task UpdateProfileInfo(ProfileInfoRequest requestData, string userId)
        {
            if(await _BlobStorageService.DeleteProfileImageAsync(userId, BlobStorageContainers.ProfileImages))
            {
                await _BlobStorageService.UploadProfileImageAsync(requestData.ProfilePhoto, BlobStorageContainers.ProfileImages, userId);
            }

            await _doctorsRepository.UpdateProfileInfo(requestData, userId);
        }
    }
}