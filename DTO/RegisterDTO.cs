using System.ComponentModel.DataAnnotations;

namespace DiaryApp.DTO
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string username { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string password0 { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string password1 { get; set; }
    }
}
