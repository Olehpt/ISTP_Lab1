using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class PublicationType:Entity
{
    public string? Description { get; set; }

    public string? Requirements { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
