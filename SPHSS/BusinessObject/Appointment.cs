using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public int StudentId { get; set; }

    public int PsychologistId { get; set; }

    public DateOnly DateCreated { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly? DateEnd { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account Psychologist { get; set; } = null!;

    public virtual ICollection<Slot> Slots { get; set; } = new List<Slot>();

    public virtual Account Student { get; set; } = null!;
}
