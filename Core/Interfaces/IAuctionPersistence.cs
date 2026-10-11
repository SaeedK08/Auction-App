namespace AuctionApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllActiveAuctions();
    Auction GetById(int id);
    void Save(Auction auction);
    void Update(Auction auction);
    void AddBid(int auctionId, Bid bid);
    List<Auction> GetOngoingAuctionsByBidder(string userName);
    List<Auction> GetWonAuctionsBy(string userName);
}