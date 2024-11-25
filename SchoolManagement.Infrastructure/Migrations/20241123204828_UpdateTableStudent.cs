using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsInfo");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Parents",
                newName: "ParentName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBarith",
                table: "Student",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfJoining",
                table: "Student",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Grade",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParentId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ParentId",
                table: "Student",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Parents_ParentId",
                table: "Student",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Parents_ParentId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_ParentId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DateOfBarith",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DateOfJoining",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "ParentName",
                table: "Parents",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "StudentsInfo",
                columns: table => new
                {
                    StudentInfoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBarith = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsInfo", x => x.StudentInfoId);
                    table.ForeignKey(
                        name: "FK_StudentsInfo_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "ParentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInfo_ParentId",
                table: "StudentsInfo",
                column: "ParentId");
        }
    }
}
