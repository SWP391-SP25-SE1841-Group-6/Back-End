using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public DateOnly DateCreated { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<ProgramSignup> ProgramSignups { get; set; } = new List<ProgramSignup>();

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
