using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Test
{
    public int TestId { get; set; }
    public string TestName { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
