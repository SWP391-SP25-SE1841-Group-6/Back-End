using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentAndSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Appointment",
                table: "Slots");

            migrationBuilder.DropForeignKey(
                name: "FK__Slots__ProgramID__46E78A0C",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_AppointmentID",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "AppointmentID",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "ProgramID",
                table: "Slots",
                newName: "ProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_ProgramID",
                table: "Slots",
                newName: "IX_Slots_ProgramId");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Slots",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "Slots",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Appointment",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Appointment",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "SlotId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_SlotId",
                table: "Appointment",
                column: "SlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Slots_SlotId",
                table: "Appointment",
                column: "SlotId",
                principalTable: "Slots",
                principalColumn: "SlotID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Program_ProgramId",
                table: "Slots",
                column: "ProgramId",
                principalTable: "Program",
                principalColumn: "Program_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Slots_SlotId",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Program_ProgramId",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_SlotId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "SlotId",
                table: "Appointment");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "Slots",
                newName: "ProgramID");

            migrationBuilder.RenameIndex(
                name: "IX_Slots_ProgramId",
                table: "Slots",
                newName: "IX_Slots_ProgramID");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Slots",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentID",
                table: "Slots",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "Slots",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<bool>(
                name: "isDeleted",
                table: "Appointment",
                type: "bit",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "Appointment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "Appointment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Slots_AppointmentID",
                table: "Slots",
                column: "AppointmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Appointment",
                table: "Slots",
                column: "AppointmentID",
                principalTable: "Appointment",
                principalColumn: "AppointmentID");

            migrationBuilder.AddForeignKey(
                name: "FK__Slots__ProgramID__46E78A0C",
                table: "Slots",
                column: "ProgramID",
                principalTable: "Program",
                principalColumn: "Program_ID");
        }
    }
}
