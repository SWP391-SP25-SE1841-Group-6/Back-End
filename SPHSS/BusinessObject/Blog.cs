using System;
using System.Collections.Generic;

namespace BusinessObject;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? BlogName { get; set; }

    public int? CreatorId { get; set; }

    public string? ContentDescription { get; set; }

    public DateTime? DateCreated { get; set; }

    public bool? IsDeleted { get; set; }
    public bool? IsApproved { get; set; }

    public virtual Account? Creator { get; set; }
}
