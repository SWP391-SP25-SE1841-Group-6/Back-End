using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<ProgramSignup> ProgramSignups { get; set; } = new List<ProgramSignup>();

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();
}
