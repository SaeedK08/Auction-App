
namespace AuctionApp.Core;

public class Bid
{
    public int Id { get; private set; }
    public string BidderName { get; private set; }
    public int Amount { get; private set; } 
    public DateTime TimePlaced { get; private set; }

    public Bid(string bidderName, int amount)
    {
        if (string.IsNullOrWhiteSpace(bidderName))
            throw new ArgumentException("Bidder name is required", nameof(bidderName));
            
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Bid amount must be greater than zero");
        
        BidderName = bidderName;
        Amount = amount;
        TimePlaced = DateTime.Now;
    }

    public Bid(int id, string bidderName, int amount, DateTime timePlaced)
    {
        Id  = id;
        BidderName = bidderName;
        Amount = amount;
        TimePlaced = timePlaced;
    }

    // public Bid() { }
}