using AutoMapper;
using ClinicManagerAPI.Constants;
using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.DTOs.Generic;
using ClinicManagerAPI.Repositories.Interfaces;
using ClinicManagerAPI.Services.DoctorProfile.Interfaces;
using ClinicManagerAPI.Services.User.Interfaces;

namespace ClinicManagerAPI.Services.DoctorProfile
{
    public class DoctorProfileService : IDoctorProfileService
    {
        private readonly IDoctorProfileRepository _doctorProfileRepository;
        private readonly IUserService userService;
        private readonly IMapper _mapper;

        public DoctorProfileService(IDoctorProfileRepository doctorProfileRepository, IUserService userService, IMapper mapper)
        {
            this._doctorProfileRepository = doctorProfileRepository;
            this.userService = userService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrieves a doctor profile by the doctor's unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A <see cref="DoctorProfileDto"/> object representing the doctor profile with the specified ID.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<DoctorProfileDto> GetDoctorProfileById(int doctorId)
        {
            var doctorProfileEntity = await _doctorProfileRepository.GetDoctorProfileById(doctorId);

            if (doctorProfileEntity == null)
                throw new KeyNotFoundException($"Doctor profile with ID {doctorId} not found.");

            return _mapper.Map<DoctorProfileDto>(doctorProfileEntity);
        }

        /// <summary>
        /// Retrieves a paged list of doctor profiles based on the provided query parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> A <see cref="PagedResult{DoctorProfileDto}"/> containing the paged list of doctor profiles.</returns>
        public async Task<PagedResult<DoctorProfileDto>> GetDoctorProfiles(DoctorProfileQueryParameters parameters)
        {
            var pagedEntities = await _doctorProfileRepository.GetDoctorProfiles(parameters);

            return new PagedResult<DoctorProfileDto>
            {
                Page = pagedEntities.Page,
                PageSize = pagedEntities.PageSize,
                TotalItems = pagedEntities.TotalItems,
                Items = _mapper.Map<List<DoctorProfileDto>>(pagedEntities.Items)
            };
        }

        /// <summary>
        /// Adds a new doctor profile.
        /// </summary>
        /// <param name="addDoctorProfileDto"></param>
        /// <returns>> A task representing the asynchronous operation.</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<DoctorProfileDto> AddDoctorProfile(AddDoctorProfileDto addDoctorProfileDto)
        {
            var existingUser = await userService.GetUserById(addDoctorProfileDto.DoctorId);

            if (existingUser.Role != UserRole.doctor)
                throw new UnauthorizedAccessException("The specified user is not a doctor.");

            var doctorProfileEntity = _mapper.Map<Models.Entities.DoctorProfileEntity>(addDoctorProfileDto);
            var resultEntity = await _doctorProfileRepository.AddDoctorProfile(doctorProfileEntity);
            return _mapper.Map<DoctorProfileDto>(resultEntity);
        }

        /// <summary>
        /// Updates an existing doctor profile.
        /// </summary>
        /// <param name="updateDoctorProfileDto"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task<DoctorProfileDto> UpdateDoctorProfile(UpdateDoctorProfileDto updateDoctorProfileDto)
        {
            var existingEntity = await _doctorProfileRepository.GetDoctorProfileById(updateDoctorProfileDto.Id);

            if (existingEntity == null)
                throw new KeyNotFoundException($"Doctor profile with ID {updateDoctorProfileDto.DoctorId} not found.");

            var updatedEntity = _mapper.Map(updateDoctorProfileDto, existingEntity);
            var resultEntity = await _doctorProfileRepository.UpdateDoctorProfile(updatedEntity);
            return _mapper.Map<DoctorProfileDto>(resultEntity);
        }

        /// <summary>
        /// Deletes a doctor profile by its unique identifier.
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns> A task representing the asynchronous operation.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public async Task DeleteDoctorProfile(int doctorId)
        {
            var existingEntity = await _doctorProfileRepository.GetDoctorProfileById(doctorId);

            if (existingEntity == null)
                throw new KeyNotFoundException($"Doctor profile with ID {doctorId} not found.");

            await _doctorProfileRepository.DeleteDoctorProfile(existingEntity);
        }
    }
}