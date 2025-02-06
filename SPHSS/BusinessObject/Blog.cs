using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Blog
{
    public string? BlogName { get; set; }

    public int? CreatorId { get; set; }

    public string? ContentDescription { get; set; }

    public DateOnly? DateCreated { get; set; }

    public bool? IsDeleted { get; set; }

    public int BlogId { get; set; }

    public virtual Account? Creator { get; set; }
}
