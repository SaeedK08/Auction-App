using System.Data;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Core.Interfaces;

public class AuctionPersistence : IAuctionPersistence
{
    private readonly IRepository<AuctionDB> _auctionRepository;
    private readonly IRepository<BidDB> _bidRepository;

    public AuctionPersistence(IRepository<AuctionDB> auctionRepository, IRepository<BidDB> bidRepository)
    {
        _auctionRepository = auctionRepository;
        _bidRepository = bidRepository;
    }
    public List<Auction> GetAllActiveAuctions()
    {
        var now =  DateTime.Now;
        var activeAuctions = _auctionRepository
                                                .Query()
                                                .Where(adb => adb.EndDate > now)
                                                .OrderBy(adb => adb.EndDate)
                                                .Select(adb => new Auction(
                                                    adb.Id,
                                                    adb.Title,
                                                    adb.Description,
                                                    adb.OwnerName,
                                                    adb.StartingPrice,
                                                    adb.EndDate,
                                                    null))
                                                .ToList();
        return activeAuctions;
    }

    public Auction GetById(int id)
    {
        AuctionDB adb = _auctionRepository.Query()
                        .Include(adb => adb.BidDbs 
                            .OrderByDescending(bid => bid.Amount))
                        .Where(adb => adb.Id == id)
                        .FirstOrDefault();

        if (adb == null) return null;
        
        List<Bid> bids = new List<Bid>();
        foreach (var bidDb in adb.BidDbs)
        {
            bids.Add(new Bid(bidDb.Id, bidDb.BidderName, bidDb.Amount, bidDb.TimePlaced));
        }
        Auction auction = new Auction(adb.Id, adb.Title, adb.Description, 
                adb.OwnerName, adb.StartingPrice, adb.EndDate, bids);


        return auction; 
    }

    public void Save(Auction auction)
    {
        AuctionDB auctionDB = new  AuctionDB()
            {
                Title = auction.Title,
                Description = auction.Description,
                OwnerName = auction.OwnerName,
                StartingPrice = auction.StartingPrice,
                EndDate = auction.EndDate
            };
        _auctionRepository.Add(auctionDB);
    }

    public void Update(Auction auction)
    {
        AuctionDB existingAdb = _auctionRepository.GetById(auction.Id);
        if (existingAdb == null)
        {
            throw new DataException("Auction not found.");
        }

        existingAdb.Description = auction.Description;
        _auctionRepository.Update(existingAdb);
    }

    public void AddBid(int auctionId, Bid bid)
    {
        BidDB bdb = new BidDB()
        {
            BidderName = bid.BidderName,
            Amount = bid.Amount,
            TimePlaced = bid.TimePlaced,
            AuctionId = auctionId
        };
        _bidRepository.Add(bdb);
    }

    public List<Auction> GetOngoingAuctionsByBidder(string userName)
    {
        var now = DateTime.Now;
        
        return _auctionRepository
            .Query()
            .Where(adb => adb.EndDate > now
                          && adb.BidDbs.Any(bid => bid.BidderName.Equals(userName)))
            .OrderBy(adb => adb.EndDate)
            .Select(adb => new Auction(
                adb.Id,
                adb.Title,
                adb.Description,
                adb.OwnerName,
                adb.StartingPrice,
                adb.EndDate,
                null))
            .ToList();
    }

    public List<Auction> GetWonAuctionsBy(string userName)
    {
        var now = DateTime.Now;
        return _auctionRepository
            .Query()
            .Where(adb => adb.EndDate <= now
                          && adb.BidDbs.Any()
                          && adb.BidDbs.OrderByDescending(b => b.Amount)
                              .FirstOrDefault().BidderName.Equals(userName))
            .OrderByDescending(adb => adb.EndDate)
            .Select(adb => new Auction(
                adb.Id,
                adb.Title,
                adb.Description,
                adb.OwnerName,
                adb.StartingPrice,
                adb.EndDate,
                null))
            .ToList();

    }
}