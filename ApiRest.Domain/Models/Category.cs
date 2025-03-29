using System;
using System.Collections.Generic;

namespace ApiRest.Domain.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Idea> Ideas { get; set; } = new List<Idea>();
}
