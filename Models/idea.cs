using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class idea
{
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public string title { get; set; } = null!;

    public string description { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public virtual ICollection<comment> comments { get; set; } = new List<comment>();

    public virtual user user { get; set; } = null!;

    public virtual ICollection<vote> votes { get; set; } = new List<vote>();

    public virtual ICollection<category> categories { get; set; } = new List<category>();
}
