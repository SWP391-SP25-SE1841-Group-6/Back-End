using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Slot
{
    public int SlotId { get; set; }
    public TimeOnly TimeStart { get; set; }
    public TimeOnly TimeEnd { get; set; }
    public bool IsDeleted { get; set; } = false;

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
