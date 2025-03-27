using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class category
{
    public Guid id { get; set; }

    public string name { get; set; } = null!;

    public virtual ICollection<idea> ideas { get; set; } = new List<idea>();
}
