namespace AuctionApp.Core;

public class Auction
{
    public const int TitleMaxLength = 128;
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string OwnerName { get; private set; }
    public int StartingPrice { get; private set; }
    public DateTime EndDate { get; private set; }

    private List<Bid> _bids = new List<Bid>();
    
    public IEnumerable<Bid> Bids => _bids;

    public Auction(string title, string description, string ownerName, 
        int startingPrice, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(ownerName))
            throw new ArgumentException("Owner is required", nameof(ownerName));
        if (string.IsNullOrWhiteSpace(description) || description.Length > TitleMaxLength*2)
            throw new ArgumentException($"Description is required and cannot exceed {TitleMaxLength*2} characters", nameof(description));
        if (string.IsNullOrWhiteSpace(title) || title.Length > TitleMaxLength)
            throw new ArgumentException($"Title is required, max {TitleMaxLength} characters", nameof(title));
        if (startingPrice <= 0)
            throw new ArgumentOutOfRangeException(nameof(startingPrice), "Price must be positive");
        if (endDate <= DateTime.Now)
            throw new ArgumentException("End date must be in the future", nameof(endDate));

        Title = title;
        Description = description;
        OwnerName = ownerName;
        StartingPrice = startingPrice;
        EndDate = endDate;
    }
    
    // When reading from the database
    public Auction(int id, string title, string description,
        string ownerName, int startingPrice, DateTime endDate, IEnumerable<Bid>? bids = null)
    {
        Id = id;
        Title = title;
        Description = description;
        OwnerName = ownerName;
        StartingPrice = startingPrice;
        EndDate = endDate;
        _bids = bids?.ToList() ?? new List<Bid>();
    }

    public Auction() { }
    

    public void UpdateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
            
        if (description.Length > TitleMaxLength * 2)
            throw new ArgumentException($"Description cannot exceed {TitleMaxLength*2} characters", nameof(description));
        
        Description = description;
    }
    
    public Bid CreateBid(string bidderName, int amount, int? highestBidAmount)
    {
        if (!IsActive())
            throw new InvalidOperationException("Auction has ended");
        if (bidderName.Equals(OwnerName))
            throw new InvalidOperationException("Cannot bid on your own auction");
        if (amount <= (highestBidAmount ?? StartingPrice))
            throw new InvalidOperationException("Bid must be higher than current price");
    
        return new Bid(bidderName, amount);
    }
    
    public bool IsActive()
    {
        return EndDate > DateTime.Now;
    }

}