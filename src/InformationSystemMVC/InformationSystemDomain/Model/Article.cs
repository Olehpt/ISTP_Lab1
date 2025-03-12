using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InformationSystemDomain.Model;

public partial class Article
{
    public int ArticleId { get; set; }
    public string Name { get; set; } = null!;

    public string Topic { get; set; } = null!;

    public string Content { get; set; } = null!;
    public DateOnly PublicationDate { get; set; }

    public byte[]? Media { get; set; }

    public int SubjectId { get; set; }

    public int TypeId { get; set; }

    public virtual ICollection<AuthorsPerArticle> AuthorsPerArticles { get; set; } = new List<AuthorsPerArticle>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Subject Subject { get; set; } = null!;

    public virtual PublicationType Type { get; set; } = null!;

}
