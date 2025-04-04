using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Program");

            migrationBuilder.RenameColumn(
                name: "DateStart",
                table: "Program",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Program",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentNumber",
                table: "Program",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PsychologistId",
                table: "Program",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Program_PsychologistId",
                table: "Program",
                column: "PsychologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Account_PsychologistId",
                table: "Program",
                column: "PsychologistId",
                principalTable: "Account",
                principalColumn: "AccID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Program_Account_PsychologistId",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Program_PsychologistId",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "CurrentNumber",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "PsychologistId",
                table: "Program");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Program",
                newName: "DateStart");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateEnd",
                table: "Program",
                type: "date",
                nullable: true);
        }
    }
}
