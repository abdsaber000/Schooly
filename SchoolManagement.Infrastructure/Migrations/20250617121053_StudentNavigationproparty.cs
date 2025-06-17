using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StudentNavigationproparty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorkSubmissions_Users_StudentId",
                table: "HomeWorkSubmissions");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorkSubmissions_Student_StudentId",
                table: "HomeWorkSubmissions",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorkSubmissions_Student_StudentId",
                table: "HomeWorkSubmissions");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorkSubmissions_Users_StudentId",
                table: "HomeWorkSubmissions",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
