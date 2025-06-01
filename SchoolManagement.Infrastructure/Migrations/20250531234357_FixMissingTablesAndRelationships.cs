using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixMissingTablesAndRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Add ResetCodes table
            migrationBuilder.CreateTable(
                name: "ResetCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    ExpirationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ResetCodes", x => x.Id); });

            // 2. Add LessonStatus (if not already in previous migration)
            migrationBuilder.AddColumn<int>(
                name: "LessonStatus",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResetCodes");


            migrationBuilder.DropColumn(
                name: "LessonStatus",
                table: "Lessons");
        }
    }
}
