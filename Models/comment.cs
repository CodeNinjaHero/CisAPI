using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class comment
{
    public Guid id { get; set; }

    public Guid user_id { get; set; }

    public Guid idea_id { get; set; }

    public string content { get; set; } = null!;

    public DateTime? created_at { get; set; }

    public virtual idea idea { get; set; } = null!;

    public virtual user user { get; set; } = null!;
}
