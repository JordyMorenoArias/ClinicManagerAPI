using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddLastModifiedFieldsToAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastModifiedById",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LastModifiedById",
                table: "Appointments",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Users_LastModifiedById",
                table: "Appointments",
                column: "LastModifiedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Users_LastModifiedById",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_LastModifiedById",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Appointments");
        }
    }
}
