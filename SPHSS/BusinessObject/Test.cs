using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Test
{
    public int TestId { get; set; }

    public string TestName { get; set; } = null!;

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}
