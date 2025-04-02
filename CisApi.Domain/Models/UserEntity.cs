using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace CisApi.Domain.Models;

public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    [MaxLength(255)]
    public string Login { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    public virtual ICollection<IdeaEntity> Ideas { get; set; }
    public virtual ICollection<CommentEntity> Comments { get; set; }
    public virtual ICollection<VoteEntity> Votes { get; set; }

    public UserEntity()
    {
        Ideas = new HashSet<IdeaEntity>();
        Comments = new HashSet<CommentEntity>();
        Votes = new HashSet<VoteEntity>();
    }
}