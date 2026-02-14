namespace ClinicManagerAPI.Constants
{
    public static class Roles
    {
        public const string Admin = nameof(UserRole.admin);
        public const string Doctor = nameof(UserRole.doctor);
        public const string Assistant = nameof(UserRole.assistant);

        public static readonly string[] AdminAndDoctor = { Admin, Doctor };

        public static readonly string[] AdminDoctorAndAssistant = { Admin, Doctor, Assistant };

        public static readonly string[] AllStaff = { Admin, Doctor, Assistant };
    }
}
