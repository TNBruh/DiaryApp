using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace DiaryApp.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [EmailAddress]
        [Required]
        public string email { get; set; }
        [RegularExpression("^[a-zA-Z0-9]*$")]
        [Required]
        public string username { get; set; }
        [RegularExpression("^[a-zA-Z0-9]*$")]
        [Required]
        [JsonIgnore]
        public string password { get; set; }
    }
}
