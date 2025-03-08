using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InformationSystemDomain.Model;

public partial class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    [Required]
    [MinLength(4, ErrorMessage = "Password must be at least 4 characters long")]
    public string Password { get; set; } = null!;

    public string? Info { get; set; }

    public string? Contacts { get; set; }

    public byte[]? Avatar { get; set; }

    public DateOnly SignUpDate { get; set; }

    public virtual ICollection<AuthorsPerArticle> AuthorsPerArticles { get; set; } = new List<AuthorsPerArticle>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();
}
