using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Data.Migrations
{
    public partial class Change_Resorse_Url_to_varchar_max : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Resource",
                type: "varchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Resource",
                type: "varchar",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(max)");
        }
    }
}
