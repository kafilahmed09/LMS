using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Server.Data.Migrations
{
    public partial class alterGrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolLevelId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SchoolLevelId",
                table: "Grades",
                column: "SchoolLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_SchoolLevels_SchoolLevelId",
                table: "Grades",
                column: "SchoolLevelId",
                principalTable: "SchoolLevels",
                principalColumn: "SchoolLevelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_SchoolLevels_SchoolLevelId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_SchoolLevelId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "SchoolLevelId",
                table: "Grades");
        }
    }
}
