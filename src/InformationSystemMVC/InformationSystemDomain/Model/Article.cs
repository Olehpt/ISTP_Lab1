﻿using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class Article:Entity
{
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
