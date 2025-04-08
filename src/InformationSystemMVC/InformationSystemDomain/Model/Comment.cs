using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InformationSystemDomain.Model;

public partial class Comment
{
    [Key]
    public int ComId { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Comment must be at most 100 characters long")]
    public string Content { get; set; } = null!;

    public DateOnly PublicationDate { get; set; }

    public int ArticleId { get; set; }

    public int AuthorId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User Author { get; set; } = null!;
}
