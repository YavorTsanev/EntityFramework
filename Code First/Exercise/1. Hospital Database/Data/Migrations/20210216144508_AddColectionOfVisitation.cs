using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_HospitalDatabase.Data.Migrations
{
    public partial class AddColectionOfVisitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Patients_PatientId1",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_PatientId1",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "Patients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId1",
                table: "Patients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientId1",
                table: "Patients",
                column: "PatientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Patients_PatientId1",
                table: "Patients",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
