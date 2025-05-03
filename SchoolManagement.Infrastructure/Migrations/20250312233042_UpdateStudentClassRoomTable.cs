using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentClassRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRooms_ClassRooms_ClassRoomId",
                table: "StudentClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRooms_Student_StudentId",
                table: "StudentClassRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRooms_ClassRooms_ClassRoomId",
                table: "StudentClassRooms",
                column: "ClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "ClassRoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRooms_Student_StudentId",
                table: "StudentClassRooms",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRooms_ClassRooms_ClassRoomId",
                table: "StudentClassRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClassRooms_Student_StudentId",
                table: "StudentClassRooms");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRooms_ClassRooms_ClassRoomId",
                table: "StudentClassRooms",
                column: "ClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClassRooms_Student_StudentId",
                table: "StudentClassRooms",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
