using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Slot
{
    public int SlotId { get; set; }

    public TimeOnly TimeStart { get; set; }

    public TimeOnly TimeEnd { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public int ProgramId { get; set; }

    public int? AppointmentId { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Program Program { get; set; } = null!;
}
