using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CisApi.Domain.Models;


public class VoteEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    [ForeignKey("Idea")]
    public Guid IdeaId { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string VoteType { get; set; } // "up" or "down"

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual UserEntity User { get; set; }
    public virtual IdeaEntity Idea { get; set; }
}