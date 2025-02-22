using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class Subject:Entity
{
    public string? Description { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
