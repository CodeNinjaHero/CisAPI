using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Idea> Ideas { get; set; } = new List<Idea>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
