using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Program_ProgramId",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_ProgramId",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "Slots");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateStart",
                table: "Program",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DateEnd",
                table: "Program",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "Program",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Program_SlotId",
                table: "Program",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Program_Slots_SlotId",
                table: "Program",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "SlotID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Program_Slots_SlotId",
                table: "Program");

            migrationBuilder.DropIndex(
                name: "IX_Program_SlotId",
                table: "Program");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Program");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "Slots",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Program",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "Program",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ProgramId",
                table: "Slots",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Program_ProgramId",
                table: "Slots",
                column: "ProgramId",
                principalTable: "Program",
                principalColumn: "Program_ID");
        }
    }
}
