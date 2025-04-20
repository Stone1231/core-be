using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    [Table("user")]
    public class User
    {
        public User()
        {
        }
        

        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("hight")]
        public int Hight { get; set; }

        [Column("birthday")]
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime Birthday { get; set; }
        
        [Column("photo")]
        public string Photo { get; set; }

        //https://stackoverflow.com/questions/39322085/how-to-save-iformfile-to-disk
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }

        [JsonPropertyName("dept")] 
        [Column("dept_id")]
        [ForeignKey("Dept")]
        public int DeptId { get; set; }
        
        [JsonIgnore]
        public virtual Dept? Dept { get; set; }    

        [JsonIgnore]
        public virtual ICollection<UserRelProj>? UserRelProjs { get; set; }
        // public virtual ICollection<Proj> Projs { get; set; }

        [JsonPropertyName("projs")]
        [NotMapped]
        public List<int> Projs { get; set; }
    }
}