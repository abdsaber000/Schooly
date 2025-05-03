using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHomeWorkTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ResetCodes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Lessons",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

           

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "HomeWorks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "HomeWorks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            

            migrationBuilder.CreateIndex(
                name: "IX_HomeWorks_lessonId",
                table: "HomeWorks",
                column: "lessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Lessons_lessonId",
                table: "HomeWorks",
                column: "lessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Lessons_lessonId",
                table: "HomeWorks");

            migrationBuilder.DropIndex(
                name: "IX_HomeWorks_lessonId",
                table: "HomeWorks");
            

            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "HomeWorks");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "HomeWorks");
            

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ResetCodes",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
