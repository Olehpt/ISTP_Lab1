﻿using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class AuthorsPerArticle
{
    public int LinkId { get; set; }
    public int Authors { get; set; }

    public int Article { get; set; }

    public virtual Article ArticleNavigation { get; set; } = null!;

    public virtual User AuthorsNavigation { get; set; } = null!;
}
