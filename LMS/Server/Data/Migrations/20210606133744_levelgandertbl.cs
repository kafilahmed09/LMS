using Microsoft.EntityFrameworkCore.Migrations;

namespace LMS.Server.Data.Migrations
{
    public partial class levelgandertbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SLevel",
                table: "Schools");

            migrationBuilder.AddColumn<int>(
                name: "GanderId",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchoolLevelId",
                table: "Schools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ganders",
                columns: table => new
                {
                    GanderlId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ganders", x => x.GanderlId);
                });

            migrationBuilder.CreateTable(
                name: "SchoolLevels",
                columns: table => new
                {
                    SchoolLevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolLevels", x => x.SchoolLevelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_GanderId",
                table: "Schools",
                column: "GanderId");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolLevelId",
                table: "Schools",
                column: "SchoolLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Ganders_GanderId",
                table: "Schools",
                column: "GanderId",
                principalTable: "Ganders",
                principalColumn: "GanderlId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_SchoolLevels_SchoolLevelId",
                table: "Schools",
                column: "SchoolLevelId",
                principalTable: "SchoolLevels",
                principalColumn: "SchoolLevelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Ganders_GanderId",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_SchoolLevels_SchoolLevelId",
                table: "Schools");

            migrationBuilder.DropTable(
                name: "Ganders");

            migrationBuilder.DropTable(
                name: "SchoolLevels");

            migrationBuilder.DropIndex(
                name: "IX_Schools_GanderId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_SchoolLevelId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "GanderId",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "SchoolLevelId",
                table: "Schools");

            migrationBuilder.AddColumn<string>(
                name: "SLevel",
                table: "Schools",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
