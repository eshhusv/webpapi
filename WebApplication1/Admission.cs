using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Admission
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int VenorCode { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
        public List<Sell> Sells { get; set; } = new();
    }
}
