using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    [Table("user_proj")]
    public class UserRelProj
    {
        [Column("user_id")]
        [Key]
        public int UserId {get; set;}
        public User User {get; set;}

        [Column("proj_id")]
        [Key]
        public int ProjId {get; set;}

        public Proj Proj {get; set;}
        
    }
}