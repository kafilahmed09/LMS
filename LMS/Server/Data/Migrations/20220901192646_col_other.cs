using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Server.Data.Migrations
{
    public partial class col_other : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Other",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Other",
                table: "AspNetUsers");
        }
    }
}
