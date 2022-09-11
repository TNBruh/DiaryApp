using System.ComponentModel.DataAnnotations;

namespace DiaryApp.DTO
{
    public class DiaryReadAllDTO
    {
        [Required]
        public int page { get; set; }
        [Required]
        public bool includeArchived { get; set; } = false;
        public string title { get; set; } = "";
    }
}
