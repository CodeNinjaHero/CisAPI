using System;
using System.Collections.Generic;

namespace ApiRest.Models;

public partial class Vote
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid IdeaId { get; set; }

    public string VoteType { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Idea Idea { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
