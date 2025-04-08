using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformationSystemDomain.Model;
[Authorize(Roles = "admin")]
public partial class Subject
{
    [Key]
    public int SubjectId { get; set; }
    public string? Description { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
