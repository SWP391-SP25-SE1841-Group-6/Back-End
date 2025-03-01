using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessObject;

public partial class SphssContext : DbContext
{
    public SphssContext()
    {
    }

    public SphssContext(DbContextOptions<SphssContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramSignup> ProgramSignups { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionType> QuestionTypes { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<TestResultAnswer> TestResultAnswers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MHOANG\\MINHHOANG; Database= SPHSS; Uid=sa; Pwd=12345;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccId).HasName("PK__Account__91CBC3989585B2E4");

            entity.ToTable("Account");

            entity.HasIndex(e => e.ParentId, "IX_Account_ParentID");

            entity.Property(e => e.AccId).HasColumnName("AccID");
            entity.Property(e => e.AccEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AccName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AccPass)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Account__ParentI__267ABA7A");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2A827AF28");

            entity.ToTable("Appointment");

            entity.HasIndex(e => e.PsychologistId, "IX_Appointment_PsychologistID");

            entity.HasIndex(e => e.StudentId, "IX_Appointment_StudentID");

            entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.PsychologistId).HasColumnName("PsychologistID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Psychologist).WithMany(p => p.AppointmentPsychologists)
                .HasForeignKey(d => d.PsychologistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Psych__412EB0B6");

            entity.HasOne(d => d.Student).WithMany(p => p.AppointmentStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Stude__4222D4EF");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK_Blog_1");

            entity.ToTable("Blog");

            entity.HasIndex(e => e.CreatorId, "IX_Blog_CreatorID");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.BlogName).HasMaxLength(100);
            entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

            entity.HasOne(d => d.Creator).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CreatorId)
                .HasConstraintName("FK_Blog_Account");
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK__Program__86CD63DA64465F33");

            entity.ToTable("Program");

            entity.Property(e => e.ProgramId).HasColumnName("Program_ID");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            entity.Property(e => e.ProgramName)
                .HasMaxLength(255)
                .HasColumnName("Program_Name");
        });

        modelBuilder.Entity<ProgramSignup>(entity =>
        {
            entity.HasKey(e => new { e.ProgramId, e.StudentId }).HasName("PK__ProgramS__32C52A793A2B692B");

            entity.ToTable("ProgramSignup");

            entity.HasIndex(e => e.ProgramId, "IX_ProgramSignup_ProgramID");

            entity.HasIndex(e => e.StudentId, "IX_ProgramSignup_StudentID");

            entity.Property(e => e.ProgramId).HasColumnName("ProgramID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramSignups)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramSi__Progr__440B1D61");

            entity.HasOne(d => d.Student).WithMany(p => p.ProgramSignups)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProgramSi__Stude__44FF419A");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8CBCB2CC60");

            entity.HasIndex(e => e.QtypeId, "IX_Questions_QTypeID");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.QtypeId).HasColumnName("QTypeID");
            entity.Property(e => e.Question1)
                .HasColumnType("text")
                .HasColumnName("Question");

            entity.HasOne(d => d.Qtype).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QtypeId)
                .HasConstraintName("FK__Questions__QType__45F365D3");
        });

        modelBuilder.Entity<QuestionType>(entity =>
        {
            entity.HasKey(e => e.QtypeId).HasName("PK__Question__8247B34D8503AEE8");

            entity.ToTable("QuestionType");

            entity.Property(e => e.QtypeId).HasColumnName("QTypeID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Qtype)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("QType");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Slots__0A124A4F583FCF37");

            //entity.HasIndex(e => e.AppointmentId, "IX_Slots_AppointmentID");

            //entity.HasIndex(e => e.ProgramId, "IX_Slots_ProgramID");

            entity.Property(e => e.SlotId).HasColumnName("SlotID");
            //entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
            //entity.Property(e => e.DayOfWeek).HasMaxLength(20);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("isDeleted");
            //entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

            //entity.HasOne(d => d.Appointment).WithMany(p => p.Slots)
            //    .HasForeignKey(d => d.AppointmentId)
            //    .HasConstraintName("FK_Slots_Appointment");

            //entity.HasOne(d => d.Program).WithMany(p => p.Slots)
            //    .HasForeignKey(d => d.ProgramId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Slots__ProgramID__46E78A0C");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__8CC331007EAF2967");

            entity.ToTable("Test");

            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DateUpdated).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.HasKey(e => new { e.TestId, e.QuestionId }).HasName("PK__TestQues__5C1F37F8F0E96F86");

            entity.ToTable("TestQuestion");

            entity.HasIndex(e => e.QuestionId, "IX_TestQuestion_QuestionID");

            entity.Property(e => e.TestId).HasColumnName("TestID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.DateAdded).HasColumnType("datetime");

            entity.HasOne(d => d.Question).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestQuest__Quest__48CFD27E");

            entity.HasOne(d => d.Test).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestQuest__TestI__49C3F6B7");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.TestResultId).HasName("PK__TestResu__E2463A673A2B3161");

            entity.ToTable("TestResult");

            entity.HasIndex(e => e.StudentId, "IX_TestResult_StudentID");

            entity.HasIndex(e => e.TestId, "IX_TestResult_TestID");

            entity.Property(e => e.TestResultId).HasColumnName("TestResultID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TestDate).HasColumnType("datetime");
            entity.Property(e => e.TestId).HasColumnName("TestID");

            entity.HasOne(d => d.Student).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__TestResul__Stude__35BCFE0A");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("FK__TestResul__TestI__4BAC3F29");
        });

        modelBuilder.Entity<TestResultAnswer>(entity =>
        {
            entity.HasKey(e => new { e.TestResultId, e.QuestionId }).HasName("PK__TestResu__329A3C9F15032BFD");

            entity.ToTable("TestResultAnswer");

            entity.HasIndex(e => e.QuestionId, "IX_TestResultAnswer_QuestionID");

            entity.Property(e => e.TestResultId).HasColumnName("TestResultID");
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

            entity.HasOne(d => d.Question).WithMany(p => p.TestResultAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestResul__Quest__3A81B327");

            entity.HasOne(d => d.TestResult).WithMany(p => p.TestResultAnswers)
                .HasForeignKey(d => d.TestResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestResul__TestR__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
