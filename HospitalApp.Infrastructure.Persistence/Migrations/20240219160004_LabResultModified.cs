using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LabResultModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_Patients_PatientId",
                table: "LabResults");

            migrationBuilder.DropIndex(
               name: "IX_LabResults_PatientId",
               table: "LabResults");

            migrationBuilder.DropColumn(
               name: "PatientId",
               table: "LabResults");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "LabResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_AppointmentId",
                table: "LabResults",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_Appointments_AppointmentId",
                table: "LabResults");

            migrationBuilder.DropIndex(
                name: "IX_LabResults_AppointmentId",
                table: "LabResults");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "LabResults");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
