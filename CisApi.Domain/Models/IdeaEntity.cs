using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CisApi.Domain.Models;

public class IdeaEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual UserEntity User { get; set; }
    public virtual ICollection<IdeaCategory> Categories { get; set; }
    public virtual ICollection<CommentEntity> Comments { get; set; }
    public virtual ICollection<VoteEntity> Votes { get; set; }

    public IdeaEntity()
    {
        Categories = new HashSet<IdeaCategory>();
        Comments = new HashSet<CommentEntity>();
        Votes = new HashSet<VoteEntity>();
    }
}