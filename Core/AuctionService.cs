
using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class AuctionService : IAuctionService
{
    private readonly IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }
    public List<Auction> GetAllActiveAuctions()
    {
        return _auctionPersistence.GetAllActiveAuctions();
    }

    public Auction GetAuctionById(int id)
    {
        Auction result = _auctionPersistence.GetById(id);
        if (result == null)
        {
            throw new KeyNotFoundException($"Auction not found");
        }
        return result;
    }

    public void CreateAuction(string title, string description, string ownerName, int startingPrice, DateTime endDate)
    {
        Auction auction = new Auction(title, description, ownerName, startingPrice, endDate);
        _auctionPersistence.Save(auction);
    }

    public void UpdateDescription(int id, string description, string requestingUser)
    {
        Auction auction = GetAuctionById(id);
        if (!auction.OwnerName.Equals(requestingUser))
        {
            throw new UnauthorizedAccessException("Only the owner is allowed to edit this auction.");
        }
        auction.UpdateDescription(description); 
    
        _auctionPersistence.Update(auction); 
    }

    public void PlaceBid(int auctionId, string bidderName, int amount)
    {
        Auction auction = GetAuctionById(auctionId);
        int? highestBidAmount = auction.Bids.Any() ?  auction.Bids.Max(b => b.Amount) : null;
        Bid bid = auction.CreateBid(bidderName, amount, highestBidAmount);
        _auctionPersistence.AddBid(auctionId, bid);
    }

    public List<Auction> GetOngoingAuctionsByBidder(string userName)
    {
        return _auctionPersistence.GetOngoingAuctionsByBidder(userName);
    }

    public List<Auction> GetWonAuctionsBy(string userName)
    {
        return _auctionPersistence.GetWonAuctionsBy(userName);
    }
}