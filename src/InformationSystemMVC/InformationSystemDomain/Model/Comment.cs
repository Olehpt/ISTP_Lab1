using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class Comment
{
    public int ComId { get; set; }
    public string Content { get; set; } = null!;

    public DateOnly PublicationDate { get; set; }

    public int ArticleId { get; set; }

    public int AuthorId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User Author { get; set; } = null!;
}
