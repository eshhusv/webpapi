using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class ModelDB : DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Sell> SellOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sell>().HasData(
                new Sell { Name="GaySell", VenorCode=000000001, SellingDate=new DateTime(28/10/2023), Id=1},
                new Sell { Name="NiggerSell", VenorCode=000000002, SellingDate=new DateTime(29/10/2023), Id=2},
                new Sell { Name="Kapysta)Sell", VenorCode=000000003, SellingDate=new DateTime(30/10/2023), Id=3},
                new Sell { Name="BananaSell", VenorCode=000000003, SellingDate=new DateTime(30/10/2023), Id=4}
                );
            modelBuilder.Entity<Admission>().HasData(
                new Admission { Name = "Gay", VenorCode = 000000005, Price = 12, Id=1 },
                new Admission { Name = "Nigger", VenorCode = 000000006, Price = 20,Id=2 },
                new Admission { Name = "Kapusta)", VenorCode = 000000007, Price = 280,Id=3 },
                new Admission { Name = "Banana", VenorCode = 000000008, Price = 140,Id=4 }
                );
        }
    }
}
