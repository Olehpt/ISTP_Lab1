using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InformationSystemDomain.Model;

public partial class PublicationType
{
    [Key]
    public int TypeId { get; set; }
    public string? Description { get; set; }

    public string? Requirements { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
