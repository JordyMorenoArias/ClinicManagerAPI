using System.ComponentModel.DataAnnotations;

namespace ClinicManagerAPI.Models.DTOs.MedicalRecord
{
    public class MedicalRecordQueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int? patientId { get; set; }

        public int? doctorId { get; set; }
    }
}