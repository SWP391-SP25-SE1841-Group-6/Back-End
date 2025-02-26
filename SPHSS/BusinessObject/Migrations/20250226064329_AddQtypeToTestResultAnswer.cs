using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddQtypeToTestResultAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    AccPass = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    AccEmail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    IsActivated = table.Column<bool>(type: "bit", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__91CBC3989585B2E4", x => x.AccID);
                    table.ForeignKey(
                        name: "FK__Account__ParentI__267ABA7A",
                        column: x => x.ParentID,
                        principalTable: "Account",
                        principalColumn: "AccID");
                });

            migrationBuilder.CreateTable(
                name: "Program",
                columns: table => new
                {
                    Program_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Program_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Program__86CD63DA64465F33", x => x.Program_ID);
                });

            migrationBuilder.CreateTable(
                name: "QuestionType",
                columns: table => new
                {
                    QTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QType = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__8247B34D8503AEE8", x => x.QTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    DateUpdated = table.Column<DateTime>(type: "datetime", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test__8CC331007EAF2967", x => x.TestID);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    PsychologistID = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Appointm__8ECDFCA2A827AF28", x => x.AppointmentID);
                    table.ForeignKey(
                        name: "FK__Appointme__Psych__412EB0B6",
                        column: x => x.PsychologistID,
                        principalTable: "Account",
                        principalColumn: "AccID");
                    table.ForeignKey(
                        name: "FK__Appointme__Stude__4222D4EF",
                        column: x => x.StudentID,
                        principalTable: "Account",
                        principalColumn: "AccID");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: true),
                    ContentDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_1", x => x.BlogID);
                    table.ForeignKey(
                        name: "FK_Blog_Account",
                        column: x => x.CreatorID,
                        principalTable: "Account",
                        principalColumn: "AccID");
                });

            migrationBuilder.CreateTable(
                name: "ProgramSignup",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProgramS__32C52A793A2B692B", x => new { x.ProgramID, x.StudentID });
                    table.ForeignKey(
                        name: "FK__ProgramSi__Progr__440B1D61",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "Program_ID");
                    table.ForeignKey(
                        name: "FK__ProgramSi__Stude__44FF419A",
                        column: x => x.StudentID,
                        principalTable: "Account",
                        principalColumn: "AccID");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QTypeID = table.Column<int>(type: "int", nullable: true),
                    Question = table.Column<string>(type: "text", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__0DC06F8CBCB2CC60", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK__Questions__QType__45F365D3",
                        column: x => x.QTypeID,
                        principalTable: "QuestionType",
                        principalColumn: "QTypeID");
                });

            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    TestResultID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: true),
                    TestID = table.Column<int>(type: "int", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestResu__E2463A673A2B3161", x => x.TestResultID);
                    table.ForeignKey(
                        name: "FK__TestResul__Stude__35BCFE0A",
                        column: x => x.StudentID,
                        principalTable: "Account",
                        principalColumn: "AccID");
                    table.ForeignKey(
                        name: "FK__TestResul__TestI__4BAC3F29",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "TestID");
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    DayOfWeek = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ProgramID = table.Column<int>(type: "int", nullable: false),
                    AppointmentID = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Slots__0A124A4F583FCF37", x => x.SlotID);
                    table.ForeignKey(
                        name: "FK_Slots_Appointment",
                        column: x => x.AppointmentID,
                        principalTable: "Appointment",
                        principalColumn: "AppointmentID");
                    table.ForeignKey(
                        name: "FK__Slots__ProgramID__46E78A0C",
                        column: x => x.ProgramID,
                        principalTable: "Program",
                        principalColumn: "Program_ID");
                });

            migrationBuilder.CreateTable(
                name: "TestQuestion",
                columns: table => new
                {
                    TestID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestQues__5C1F37F8F0E96F86", x => new { x.TestID, x.QuestionID });
                    table.ForeignKey(
                        name: "FK__TestQuest__Quest__48CFD27E",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                    table.ForeignKey(
                        name: "FK__TestQuest__TestI__49C3F6B7",
                        column: x => x.TestID,
                        principalTable: "Test",
                        principalColumn: "TestID");
                });

            migrationBuilder.CreateTable(
                name: "TestResultAnswer",
                columns: table => new
                {
                    TestResultID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<int>(type: "int", nullable: true),
                    Qtype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestResu__329A3C9F15032BFD", x => new { x.TestResultID, x.QuestionID });
                    table.ForeignKey(
                        name: "FK__TestResul__Quest__3A81B327",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                    table.ForeignKey(
                        name: "FK__TestResul__TestR__398D8EEE",
                        column: x => x.TestResultID,
                        principalTable: "TestResult",
                        principalColumn: "TestResultID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ParentID",
                table: "Account",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_PsychologistID",
                table: "Appointment",
                column: "PsychologistID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_StudentID",
                table: "Appointment",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_CreatorID",
                table: "Blog",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSignup_ProgramID",
                table: "ProgramSignup",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSignup_StudentID",
                table: "ProgramSignup",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QTypeID",
                table: "Questions",
                column: "QTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_AppointmentID",
                table: "Slots",
                column: "AppointmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_ProgramID",
                table: "Slots",
                column: "ProgramID");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_QuestionID",
                table: "TestQuestion",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_StudentID",
                table: "TestResult",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TestID",
                table: "TestResult",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_TestResultAnswer_QuestionID",
                table: "TestResultAnswer",
                column: "QuestionID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "ProgramSignup");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "TestQuestion");

            migrationBuilder.DropTable(
                name: "TestResultAnswer");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Program");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "TestResult");

            migrationBuilder.DropTable(
                name: "QuestionType");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Test");
        }
    }
}
