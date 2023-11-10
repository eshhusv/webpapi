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
                new Sell { Name="GaySell", VenorCode=000000001, SellingDate=new DateTime(28/10/2023), Id=1},
                new Sell { Name="NiggerSell", VenorCode=000000002, SellingDate=new DateTime(29/10/2023), Id=2},
                new Sell { Name="Kapysta)Sell", VenorCode=000000003, SellingDate=new DateTime(30/10/2023), Id=3},
                new Sell { Name="BananaSell", VenorCode=000000003, SellingDate=new DateTime(30/10/2023), Id=4},
                new Sell { Name="LeafSell", VenorCode=000000005, SellingDate=new DateTime(30/10/2023), Id=5},
                new Sell { Name="UrmomSell", VenorCode=000000006, SellingDate=new DateTime(30/10/2023), Id=6},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000007, SellingDate=new DateTime(30/10/2023), Id=7},
                new Sell { Name= "NuclearWeaponSell", VenorCode=000000008, SellingDate=new DateTime(30/10/2023), Id=8},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000009, SellingDate=new DateTime(30/10/2023), Id=9},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000010, SellingDate=new DateTime(30/10/2023), Id=10},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000011, SellingDate=new DateTime(30/10/2023), Id=11},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000012, SellingDate=new DateTime(30/10/2023), Id=12},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000013, SellingDate=new DateTime(30/10/2023), Id=13},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000014, SellingDate=new DateTime(30/10/2023), Id=14},
                new Sell { Name="NuclearWeaponSell", VenorCode=000000015, SellingDate=new DateTime(30/10/2023), Id=15}
                );
            modelBuilder.Entity<Admission>().HasData(
                new Admission { Name = "Gay", VenorCode = 000000001, Price = 12 },
                new Admission { Name = "Nigger", VenorCode = 000000002, Price = 20 },
                new Admission { Name = "Kapusta)", VenorCode = 000000003, Price = 280 },
                new Admission { Name = "Banana", VenorCode = 000000004, Price = 140},
                new Admission { Name = "Leaf", VenorCode = 000000005, Price = 140 },
                new Admission { Name = "Urmom", VenorCode = 000000006, Price = 140 },
                new Admission { Name = "NuclearWeapon", VenorCode = 000000007, Price = 140 }
                );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, EMail = "Z@gmail.com", Password = "Zalupa" });
        }
    }
}
