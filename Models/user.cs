using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class user
{
    public Guid id { get; set; }

    public string? name { get; set; }

    public string? login { get; set; }

    public string? password { get; set; }

    public virtual ICollection<comment> comments { get; set; } = new List<comment>();

    public virtual ICollection<idea> ideas { get; set; } = new List<idea>();

    public virtual ICollection<vote> votes { get; set; } = new List<vote>();
}
