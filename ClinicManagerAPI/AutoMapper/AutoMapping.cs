using AutoMapper;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Models.Entities;
using EcommerceAPI.Models.DTOs.User;

namespace ClinicManagerAPI.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // user mappings
            CreateMap<UserEntity, UserDto>().ReverseMap();
            CreateMap<UserEntity, UserRegisterDto>().ReverseMap();
            CreateMap<UserEntity, UserUpdateDto>().ReverseMap();

            // Patient mappings
            CreateMap<PatientEntity, PatientDto>().ReverseMap();

            // Appointment mappings
            CreateMap<AppointmentEntity, AppointmentDto>().ReverseMap();

            // Medical Record mappings
            CreateMap<MedicalRecordEntity, MedicalRecordDto>().ReverseMap();
        }
    }
}