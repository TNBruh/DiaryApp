
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiaryApp.Models
{
    public class Diary
    {
        [Key]
        public int id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string title { get; set; }
        [Required]
        [StringLength(500)]
        public string content { get; set; }
        public bool isArchived { get; set; } = false;
        public DateTime created { get; set; } = DateTime.Now;
        public DateTime updated { get; set; } = DateTime.Now;
        [Display(Name = "User")]
        public int userID { get; set; }
        [ForeignKey("userID")]
        [JsonIgnore]
        public virtual User user { get; set; }

    }
}
