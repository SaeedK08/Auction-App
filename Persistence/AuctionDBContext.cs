using AuctionApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence
{
    public class AuctionDBContext : DbContext
    {
        public AuctionDBContext(DbContextOptions<AuctionDBContext> options) : base(options) { }

        public DbSet<AuctionDB> Auctions { get; set; }
        public DbSet<BidDB> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Valfritt: Här kan vi lägga in Seed Data så vi slipper ha en tom databas!
            // Exakt som läraren gjorde med ProjectDb i tutorialen.
            
            AuctionDB a1 = new AuctionDB
            {
                Id = -1,
                Title = "Antik Vas",
                Description = "En mycket fin vas från Ming-dynastin.",
                OwnerName = "Kalle",
                StartingPrice = 500,
                EndDate = DateTime.Now.AddDays(5)
            };

            modelBuilder.Entity<AuctionDB>().HasData(a1);

            BidDB b1 = new BidDB
            {
                Id = -1,
                BidderName = "Anna",
                Amount = 600,
                TimePlaced = DateTime.Now.AddHours(-2),
                AuctionId = -1 // Foreign Key till a1
            };

            BidDB b2 = new BidDB
            {
                Id = -2,
                BidderName = "Oskar",
                Amount = 750,
                TimePlaced = DateTime.Now.AddHours(-1),
                AuctionId = -1 // Foreign Key till a1
            };
            modelBuilder.Entity<BidDB>().HasData(b1, b2);
        }
    }
}