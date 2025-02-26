using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class TestResultAnswer
{
    public int TestResultId { get; set; }

    public int QuestionId { get; set; }

    public int? Answer { get; set; }
    public string? Qtype { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual TestResult TestResult { get; set; } = null!;
}
