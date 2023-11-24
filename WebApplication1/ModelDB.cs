using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class ModelDB : DbContext
    {
        public ModelDB(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Sell> SellOrders { get; set; }
        public DbSet<User>? Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sell>().HasData(
                new Sell { Name="Adm1Sell", VenorCode=1, SellingDate=new DateTime(28/10/2023), Id=1},
                new Sell { Name="Adm2Sell", VenorCode=2, SellingDate=new DateTime(29/10/2023), Id=2},
                new Sell { Name="Kapysta)Sell", VenorCode=3, SellingDate=new DateTime(30/10/2023), Id=3},
                new Sell { Name="BananaSell", VenorCode=3, SellingDate=new DateTime(30/10/2023), Id=4},
                new Sell { Name="LeafSell", VenorCode=5, SellingDate=new DateTime(30/10/2023), Id=5},
                new Sell { Name="UrmomSell", VenorCode=6, SellingDate=new DateTime(30/10/2023), Id=6},
                new Sell { Name="NuclearWeaponSell", VenorCode=7, SellingDate=new DateTime(30/10/2023), Id=7},
                new Sell { Name= "NuclearWeaponSell", VenorCode=8, SellingDate=new DateTime(30/10/2023), Id=8},
                new Sell { Name="NuclearWeaponSell", VenorCode=9, SellingDate=new DateTime(30/10/2023), Id=9},
                new Sell { Name="NuclearWeaponSell", VenorCode=10, SellingDate=new DateTime(30/10/2023), Id=10},
                new Sell { Name="NuclearWeaponSell", VenorCode=11, SellingDate=new DateTime(30/10/2023), Id=11},
                new Sell { Name="NuclearWeaponSell", VenorCode=12, SellingDate=new DateTime(30/10/2023), Id=12},
                new Sell { Name="NuclearWeaponSell", VenorCode=13, SellingDate=new DateTime(30/10/2023), Id=13},
                new Sell { Name="NuclearWeaponSell", VenorCode=14, SellingDate=new DateTime(30/10/2023), Id=14},
                new Sell { Name="NuclearWeaponSell", VenorCode=15, SellingDate=new DateTime(30/10/2023), Id=15}
                );
            modelBuilder.Entity<Admission>().HasData(
                new Admission { Id = 1, Name = "Adm1", VenorCode = 1, Price = 12 },
                new Admission { Id = 2, Name = "Adm2", VenorCode = 2, Price = 20 },
                new Admission { Id = 3, Name = "Kapusta)", VenorCode = 3, Price = 280 },
                new Admission { Id = 4, Name = "Banana", VenorCode = 4, Price = 140},
                new Admission { Id = 5, Name = "Leaf", VenorCode = 5, Price = 140 },
                new Admission { Id = 6, Name = "Urmom", VenorCode = 6, Price = 140 },
                new Admission { Id = 7, Name = "NuclearWeapon", VenorCode = 7, Price = 140 }
                );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, EMail = "Z@gmail.com", Password = "123" });
        }
    }
}
