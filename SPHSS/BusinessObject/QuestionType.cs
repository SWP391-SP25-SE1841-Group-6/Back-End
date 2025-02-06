using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class QuestionType
{
    public int QtypeId { get; set; }

    public string? Qtype { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
