using System.ComponentModel.DataAnnotations;

namespace WebApplication1
{
    public class Sell
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime SellingDate { get; set; }
        [Required]
        public int VenorCode { get; set; }
        [Required]
        public string? Name { get; set; }
        public int CountOfSold { get; set; }
        public double PriceOfSold { get; set; }
    }
}
