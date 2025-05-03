using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClassRoomForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassRoomId",
                table: "ClassRooms",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassRoomId",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ClassRoomId",
                table: "Posts",
                column: "ClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_ClassRooms_ClassRoomId",
                table: "Posts",
                column: "ClassRoomId",
                principalTable: "ClassRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_ClassRooms_ClassRoomId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ClassRoomId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ClassRoomId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ClassRooms",
                newName: "ClassRoomId");
        }
    }
}
