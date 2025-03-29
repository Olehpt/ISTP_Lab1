using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformationSystemDomain.Model;

public partial class Subject
{
    public int SubjectId { get; set; }
    public string? Description { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
