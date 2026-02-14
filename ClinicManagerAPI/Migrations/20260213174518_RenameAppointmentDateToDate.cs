using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameAppointmentDateToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Appointments",
                newName: "AppointmentDate");
        }
    }
}
