using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject;

public partial class Program
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "date")]
    public DateOnly Date { get; set; }

    public bool? IsDeleted { get; set; }

    public int SlotId { get; set; }
    public string? GoogleMeetLink { get; set; }

    public Slot Slot { get; set; } = null!;

    public virtual ICollection<ProgramSignup> ProgramSignups { get; set; } = new List<ProgramSignup>();

    public int PsychologistId { get; set; }

    public virtual Account Psychologist { get; set; } = null!;

    public int Capacity { get; set; }

    public int? CurrentNumber { get; set; }

}
