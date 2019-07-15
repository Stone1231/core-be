using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    [Table("dept")]
    public class Dept
    {
        public Dept()
        {
        }
        
        [Column("id")]
        [Key]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}