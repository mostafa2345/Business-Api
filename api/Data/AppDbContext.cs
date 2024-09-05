using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
            modelBuilder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.portfolios)
            .HasForeignKey(p => p.AppUserId);

            modelBuilder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(u => u.portfolios)
            .HasForeignKey(p => p.StockId);








            //  modelBuilder.Entity<Stock>().HasData(Generte());
            List<IdentityRole> roles = new List<IdentityRole>{
            new IdentityRole{
                Name = "Admin",
                NormalizedName="ADMIN",
            },
              new IdentityRole{
                Name = "User",
                NormalizedName="USER",
            }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
        public DbSet<Stock> stocks { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }
        public List<Stock> Generte()
        {
            var stockFaker = new Faker<Stock>()
                .RuleFor(s => s.Id, f => f.UniqueIndex + 1)
                .RuleFor(s => s.Symbol, f => f.Finance.Currency().Code)
                .RuleFor(s => s.CompanyName, f => f.Company.CompanyName())
                .RuleFor(s => s.Purchase, f => f.Finance.Amount(10, 100))
                .RuleFor(s => s.LastDiv, f => f.Finance.Amount(1, 5))
                .RuleFor(s => s.Industry, f => f.Company.CompanySuffix())
                .RuleFor(s => s.MarketCap, f => f.Random.Long(1_000_000, 10_000_000));

            var fakeStock = stockFaker.Generate(20);

            return fakeStock;
        }



    }

}