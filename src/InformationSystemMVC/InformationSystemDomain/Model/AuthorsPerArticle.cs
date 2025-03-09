using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InformationSystemDomain.Model;

public partial class AuthorsPerArticle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LinkId { get; set; }
    public int Authors { get; set; }

    public int Article { get; set; }

    public virtual Article ArticleNavigation { get; set; } = null!;

    public virtual User AuthorsNavigation { get; set; } = null!;
}
