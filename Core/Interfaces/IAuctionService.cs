namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllActiveAuctions();
    Auction GetAuctionById(int id);
    void CreateAuction(string title, string description, string ownerName, int startingPrice, DateTime endDate);
    void UpdateDescription(int id, string description, string requestingUser);
    void PlaceBid(int auctionId, string bidderName, int amount);
    List<Auction> GetOngoingAuctionsByBidder(string userName);
    List<Auction> GetWonAuctionsBy(string userName);
    // Fler metoder för budgivning etc. kommer vi lägga till senare
}