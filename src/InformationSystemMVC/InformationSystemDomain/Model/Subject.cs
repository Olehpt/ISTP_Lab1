using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class Subject
{
    public int SubjectId { get; set; }
    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
