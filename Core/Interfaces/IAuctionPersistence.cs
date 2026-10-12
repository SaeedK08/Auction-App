namespace AuctionApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllActiveAuctions();
    Auction GetAuctionById(int id);
    void AddAuction(Auction auction);
    void UpdateAuction(Auction auction);
    void AddBid(int auctionId, Bid bid);
    List<Auction> GetOngoingAuctionsByBidder(string userName);
    List<Auction> GetWonAuctionsBy(string userName);
    List<Auction> GetAuctionsByOwnerName(string userName);
    void DeleteAuction(int id);
    void DeleteAllDataForUser(string userName);
}