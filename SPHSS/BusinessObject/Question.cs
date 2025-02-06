using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Question
{
    public int QuestionId { get; set; }

    public int? QtypeId { get; set; }

    public string? Question1 { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual QuestionType? Qtype { get; set; }

    public virtual ICollection<TestResultAnswer> TestResultAnswers { get; set; } = new List<TestResultAnswer>();

    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
}
