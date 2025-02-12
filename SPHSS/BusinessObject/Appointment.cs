using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int StudentId { get; set; }

    public int PsychologistId { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account Psychologist { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual Account Student { get; set; } = null!;
}
