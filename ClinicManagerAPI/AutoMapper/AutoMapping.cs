using AutoMapper;
using ClinicManagerAPI.Models.DTOs;
using ClinicManagerAPI.Models.DTOs.Allergy;
using ClinicManagerAPI.Models.DTOs.Appointment;
using ClinicManagerAPI.Models.DTOs.DoctorProfile;
using ClinicManagerAPI.Models.DTOs.MedicalRecord;
using ClinicManagerAPI.Models.DTOs.Patient;
using ClinicManagerAPI.Models.DTOs.PatientAllergy;
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
            CreateMap<UserEntity, RegisterUserDto>().ReverseMap();
            CreateMap<UserEntity, UpdateUserDto>().ReverseMap();
            CreateMap<UserEntity, GenerateUserTokenDto>().ReverseMap();

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

            // Doctor Profile mappings
            CreateMap<DoctorProfileEntity, DoctorProfileDto>().ReverseMap();
            CreateMap<DoctorProfileEntity, AddDoctorProfileDto>().ReverseMap();
            CreateMap<DoctorProfileEntity, UpdateDoctorProfileDto>().ReverseMap();

            // Allergy Profile mappings
            CreateMap<AllergyEntity, AllergyDto>().ReverseMap();
            CreateMap<AllergyEntity, CreateAllergyDto>().ReverseMap();
            CreateMap<AllergyEntity, UpdateAllergyDto>().ReverseMap();

            // Patient Allergy mappings
            CreateMap<PatientAllergyEntity, PatientAllergyDto>().ReverseMap();
            CreateMap<PatientAllergyEntity, AddPatientAllergyDto>().ReverseMap();
            CreateMap<PatientAllergyEntity, UpdatePatientAllergyDto>().ReverseMap();
        }
    }
}