using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject;

public partial class Appointment
{
    public int AppointmentId { get; set; }
    public int StudentId { get; set; }
    public int PsychologistId { get; set; }

    [ForeignKey("Slot")]
    public int SlotId { get; set; }

    public DateOnly Date { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
    public string? GoogleMeetLink { get; set; }

    public virtual Account Psychologist { get; set; } = null!;
    public virtual Account Student { get; set; } = null!;
    public virtual Slot Slot { get; set; } = null!;
}
