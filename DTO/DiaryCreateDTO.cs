using System.ComponentModel.DataAnnotations;

namespace DiaryApp.DTO
{
    public class DiaryCreateDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string title { get; set; }
        [Required]
        [StringLength(500)]
        public string content { get; set; }
    }
}
