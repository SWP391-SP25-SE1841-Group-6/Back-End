﻿// <auto-generated />
using System;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(SphssContext))]
    [Migration("20250212093220_first")]
    partial class first
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.Account", b =>
                {
                    b.Property<int>("AccId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AccID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccId"));

                    b.Property<string>("AccEmail")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AccName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AccPass")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime?>("Dob")
                        .HasColumnType("datetime")
                        .HasColumnName("DOB");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("ParentID");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    b.HasKey("AccId")
                        .HasName("PK__Account__91CBC3989585B2E4");

                    b.HasIndex(new[] { "ParentId" }, "IX_Account_ParentID");

                    b.HasIndex(new[] { "RoleId" }, "IX_Account_RoleID");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AppointmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("isDeleted");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int")
                        .HasColumnName("PsychologistID");

                    b.Property<int>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    b.HasKey("AppointmentId")
                        .HasName("PK__Appointm__8ECDFCA2A827AF28");

                    b.HasIndex(new[] { "PsychologistId" }, "IX_Appointment_PsychologistID");

                    b.HasIndex(new[] { "StudentId" }, "IX_Appointment_StudentID");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BlogID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"));

                    b.Property<string>("BlogName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ContentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int")
                        .HasColumnName("CreatorID");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("BlogId")
                        .HasName("PK_Blog_1");

                    b.HasIndex(new[] { "CreatorId" }, "IX_Blog_CreatorID");

                    b.ToTable("Blog", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Program", b =>
                {
                    b.Property<int>("ProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Program_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgramId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("isDeleted");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Program_Name");

                    b.HasKey("ProgramId")
                        .HasName("PK__Program__86CD63DA64465F33");

                    b.ToTable("Program", (string)null);
                });

            modelBuilder.Entity("BusinessObject.ProgramSignup", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int")
                        .HasColumnName("ProgramID");

                    b.HasKey("StudentId")
                        .HasName("PK__ProgramS__32C52A793A2B692B");

                    b.HasIndex(new[] { "ProgramId" }, "IX_ProgramSignup_ProgramID");

                    b.ToTable("ProgramSignup", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("QuestionID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuestionId"));

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.Property<int?>("QtypeId")
                        .HasColumnType("int")
                        .HasColumnName("QTypeID");

                    b.Property<string>("Question1")
                        .HasColumnType("text")
                        .HasColumnName("Question");

                    b.HasKey("QuestionId")
                        .HasName("PK__Question__0DC06F8CBCB2CC60");

                    b.HasIndex(new[] { "QtypeId" }, "IX_Questions_QTypeID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("BusinessObject.QuestionType", b =>
                {
                    b.Property<int>("QtypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("QTypeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QtypeId"));

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.Property<string>("Qtype")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("QType");

                    b.HasKey("QtypeId")
                        .HasName("PK__Question__8247B34D8503AEE8");

                    b.ToTable("QuestionType", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RoleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.Property<string>("RoleName")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("RoleId")
                        .HasName("PK__Role__8AFACE3A856FF23A");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Slot", b =>
                {
                    b.Property<int>("SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SlotID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotId"));

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int")
                        .HasColumnName("AppointmentID");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool?>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("isDeleted");

                    b.Property<int>("ProgramId")
                        .HasColumnType("int")
                        .HasColumnName("ProgramID");

                    b.Property<TimeOnly>("TimeEnd")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("TimeStart")
                        .HasColumnType("time");

                    b.HasKey("SlotId")
                        .HasName("PK__Slots__0A124A4F583FCF37");

                    b.HasIndex(new[] { "AppointmentId" }, "IX_Slots_AppointmentID");

                    b.HasIndex(new[] { "ProgramId" }, "IX_Slots_ProgramID");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("BusinessObject.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TestID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestId"));

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DateUpdated")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.HasKey("TestId")
                        .HasName("PK__Test__8CC331007EAF2967");

                    b.ToTable("Test", (string)null);
                });

            modelBuilder.Entity("BusinessObject.TestResult", b =>
                {
                    b.Property<int>("TestResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TestResultID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TestResultId"));

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.Property<int?>("Score")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    b.Property<DateTime?>("TestDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("TestId")
                        .HasColumnType("int")
                        .HasColumnName("TestID");

                    b.HasKey("TestResultId")
                        .HasName("PK__TestResu__E2463A673A2B3161");

                    b.HasIndex(new[] { "StudentId" }, "IX_TestResult_StudentID");

                    b.HasIndex(new[] { "TestId" }, "IX_TestResult_TestID");

                    b.ToTable("TestResult", (string)null);
                });

            modelBuilder.Entity("BusinessObject.TestResultAnswer", b =>
                {
                    b.Property<int>("TestResultId")
                        .HasColumnType("int")
                        .HasColumnName("TestResultID");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int")
                        .HasColumnName("QuestionID");

                    b.Property<int?>("Answer")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("isDeleted");

                    b.HasKey("TestResultId", "QuestionId")
                        .HasName("PK__TestResu__329A3C9F15032BFD");

                    b.HasIndex(new[] { "QuestionId" }, "IX_TestResultAnswer_QuestionID");

                    b.ToTable("TestResultAnswer", (string)null);
                });

            modelBuilder.Entity("TestQuestion", b =>
                {
                    b.Property<int>("TestId")
                        .HasColumnType("int")
                        .HasColumnName("TestID");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int")
                        .HasColumnName("QuestionID");

                    b.HasKey("TestId", "QuestionId")
                        .HasName("PK__TestQues__5C1F37F8F0E96F86");

                    b.HasIndex(new[] { "QuestionId" }, "IX_TestQuestion_QuestionID");

                    b.ToTable("TestQuestion", (string)null);
                });

            modelBuilder.Entity("BusinessObject.Account", b =>
                {
                    b.HasOne("BusinessObject.Account", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK__Account__ParentI__267ABA7A");

                    b.HasOne("BusinessObject.Role", "Role")
                        .WithMany("Accounts")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__Account__RoleID__276EDEB3");

                    b.Navigation("Parent");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObject.Appointment", b =>
                {
                    b.HasOne("BusinessObject.Account", "Psychologist")
                        .WithMany("AppointmentPsychologists")
                        .HasForeignKey("PsychologistId")
                        .IsRequired()
                        .HasConstraintName("FK__Appointme__Psych__412EB0B6");

                    b.HasOne("BusinessObject.Account", "Student")
                        .WithMany("AppointmentStudents")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__Appointme__Stude__4222D4EF");

                    b.Navigation("Psychologist");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BusinessObject.Blog", b =>
                {
                    b.HasOne("BusinessObject.Account", "Creator")
                        .WithMany("Blogs")
                        .HasForeignKey("CreatorId")
                        .HasConstraintName("FK_Blog_Account");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("BusinessObject.ProgramSignup", b =>
                {
                    b.HasOne("BusinessObject.Program", "Program")
                        .WithMany("ProgramSignups")
                        .HasForeignKey("ProgramId")
                        .IsRequired()
                        .HasConstraintName("FK__ProgramSi__Progr__440B1D61");

                    b.HasOne("BusinessObject.Account", "Student")
                        .WithOne("ProgramSignup")
                        .HasForeignKey("BusinessObject.ProgramSignup", "StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__ProgramSi__Stude__44FF419A");

                    b.Navigation("Program");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("BusinessObject.Question", b =>
                {
                    b.HasOne("BusinessObject.QuestionType", "Qtype")
                        .WithMany("Questions")
                        .HasForeignKey("QtypeId")
                        .HasConstraintName("FK__Questions__QType__45F365D3");

                    b.Navigation("Qtype");
                });

            modelBuilder.Entity("BusinessObject.Slot", b =>
                {
                    b.HasOne("BusinessObject.Appointment", "Appointment")
                        .WithMany("Slots")
                        .HasForeignKey("AppointmentId")
                        .HasConstraintName("FK_Slots_Appointment");

                    b.HasOne("BusinessObject.Program", "Program")
                        .WithMany("Slots")
                        .HasForeignKey("ProgramId")
                        .IsRequired()
                        .HasConstraintName("FK__Slots__ProgramID__46E78A0C");

                    b.Navigation("Appointment");

                    b.Navigation("Program");
                });

            modelBuilder.Entity("BusinessObject.TestResult", b =>
                {
                    b.HasOne("BusinessObject.Account", "Student")
                        .WithMany("TestResults")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK__TestResul__Stude__35BCFE0A");

                    b.HasOne("BusinessObject.Test", "Test")
                        .WithMany("TestResults")
                        .HasForeignKey("TestId")
                        .HasConstraintName("FK__TestResul__TestI__4BAC3F29");

                    b.Navigation("Student");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("BusinessObject.TestResultAnswer", b =>
                {
                    b.HasOne("BusinessObject.Question", "Question")
                        .WithMany("TestResultAnswers")
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("FK__TestResul__Quest__3A81B327");

                    b.HasOne("BusinessObject.TestResult", "TestResult")
                        .WithMany("TestResultAnswers")
                        .HasForeignKey("TestResultId")
                        .IsRequired()
                        .HasConstraintName("FK__TestResul__TestR__398D8EEE");

                    b.Navigation("Question");

                    b.Navigation("TestResult");
                });

            modelBuilder.Entity("TestQuestion", b =>
                {
                    b.HasOne("BusinessObject.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .IsRequired()
                        .HasConstraintName("FK__TestQuest__Quest__48CFD27E");

                    b.HasOne("BusinessObject.Test", null)
                        .WithMany()
                        .HasForeignKey("TestId")
                        .IsRequired()
                        .HasConstraintName("FK__TestQuest__TestI__49C3F6B7");
                });

            modelBuilder.Entity("BusinessObject.Account", b =>
                {
                    b.Navigation("AppointmentPsychologists");

                    b.Navigation("AppointmentStudents");

                    b.Navigation("Blogs");

                    b.Navigation("InverseParent");

                    b.Navigation("ProgramSignup");

                    b.Navigation("TestResults");
                });

            modelBuilder.Entity("BusinessObject.Appointment", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("BusinessObject.Program", b =>
                {
                    b.Navigation("ProgramSignups");

                    b.Navigation("Slots");
                });

            modelBuilder.Entity("BusinessObject.Question", b =>
                {
                    b.Navigation("TestResultAnswers");
                });

            modelBuilder.Entity("BusinessObject.QuestionType", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("BusinessObject.Role", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("BusinessObject.Test", b =>
                {
                    b.Navigation("TestResults");
                });

            modelBuilder.Entity("BusinessObject.TestResult", b =>
                {
                    b.Navigation("TestResultAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
