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

    public virtual ICollection<TestQuestion> TestQuestions { get; set; } = new List<TestQuestion>();

    
}
