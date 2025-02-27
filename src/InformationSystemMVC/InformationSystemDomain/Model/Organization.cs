using System;
using System.Collections.Generic;

namespace InformationSystemDomain.Model;

public partial class Organization
{
    public int OrgId { get; set; }
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Contacts { get; set; }

    public string Country { get; set; } = null!;

    public int MembersId { get; set; }

    public virtual User Members { get; set; } = null!;
}
