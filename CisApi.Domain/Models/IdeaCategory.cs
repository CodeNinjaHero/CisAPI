using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CisApi.Domain.Models;

public class IdeaCategory
{
    [Column(Order = 0)]
    [ForeignKey("Idea")]
    public Guid IdeaId { get; set; }

    [Column(Order = 1)]
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }

    public virtual IdeaEntity Idea { get; set; }
    public virtual CategoryEntity Category { get; set; }
}
