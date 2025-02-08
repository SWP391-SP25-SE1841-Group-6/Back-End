using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Account
{
    public int AccId { get; set; }

    public string? AccName { get; set; }

    public string? AccPass { get; set; }

    public string? AccEmail { get; set; }

    public DateTime? Dob { get; set; }

    public bool? Gender { get; set; }

    public int? ParentId { get; set; }

    public int RoleId { get; set; }

    public bool? IsActivated { get; set; }

    public bool? IsApproved { get; set; }

    public virtual ICollection<Appointment> AppointmentPsychologists { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentStudents { get; set; } = new List<Appointment>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Account> InverseParent { get; set; } = new List<Account>();

    public virtual Account? Parent { get; set; }

    public virtual ProgramSignup? ProgramSignup { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
