using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class TestResult
{
    public int TestResultId { get; set; }

    public int? StudentId { get; set; }

    public int? TestId { get; set; }

    public DateTime? TestDate { get; set; }

    public int? Score { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Account? Student { get; set; }

    public virtual Test? Test { get; set; }

    public virtual ICollection<TestResultAnswer> TestResultAnswers { get; set; } = new List<TestResultAnswer>();

    public virtual ICollection<TestResultDetail> TestResultDetails { get; set; } = new List<TestResultDetail>();
}
