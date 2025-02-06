using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class ProgramSignup
{
    public int StudentId { get; set; }

    public int ProgramId { get; set; }

    public virtual Program Program { get; set; } = null!;

    public virtual Account Student { get; set; } = null!;
}
