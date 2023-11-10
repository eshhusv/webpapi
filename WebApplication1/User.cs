using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? EMail { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
