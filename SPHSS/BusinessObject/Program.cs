using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    [Column(TypeName = "date")]
    public DateOnly DateStart { get; set; }

    [Column(TypeName = "date")]
    public DateOnly? DateEnd { get; set; }

    public bool? IsDeleted { get; set; }

    public int SlotId { get; set; }
    public string? GoogleMeetLink { get; set; }

    public Slot Slot { get; set; } = null!;

    public virtual ICollection<ProgramSignup> ProgramSignups { get; set; } = new List<ProgramSignup>();

}
