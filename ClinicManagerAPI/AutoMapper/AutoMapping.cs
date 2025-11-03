using AutoMapper;
using ClinicManagerAPI.Models.DTOs;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.User;
using ClinicManagerAPI.Models.Entities;

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
            CreateMap<UserEntity, UserGenerateTokenDto>().ReverseMap();

            // Patient mappings
            CreateMap<PatientEntity, PatientDto>().ReverseMap();
            CreateMap<PatientEntity, AddPatientDto>().ReverseMap();
            CreateMap<PatientEntity, UpdatePatientDto>().ReverseMap();

            // Appointment mappings
            CreateMap<AppointmentEntity, AppointmentDto>().ReverseMap();
            CreateMap<AppointmentEntity, AddAppointmentDto>().ReverseMap();
            CreateMap<AppointmentEntity, UpdateAppointmentDto>().ReverseMap();

            // Medical Record mappings
            CreateMap<MedicalRecordEntity, MedicalRecordDto>().ReverseMap();
            CreateMap<MedicalRecordEntity, AddMedicalRecordDto>().ReverseMap();
            CreateMap<MedicalRecordEntity, UpdateMedicalRecordDto>().ReverseMap();
        }
    }
}