using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class Idea
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
