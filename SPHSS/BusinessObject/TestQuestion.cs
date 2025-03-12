using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class TestQuestion
{
    public int TestQuestionId { get; set; }
    public int TestId { get; set; }

    public int QuestionId { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual Test Test { get; set; } = null!;
    public virtual ICollection<TestResultAnswer> TestResultAnswers { get; set; } = new List<TestResultAnswer>();
}
