using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Display(Name = "Owner Name")]
    public string OwnerName { get; set; }
    public bool CanBid { get; set; }
    public bool HasEnded { get; set; }
    
    [Display(Name = "Staring Price")]
    [DisplayFormat(DataFormatString = "{0:C0}")]
    public double StartingPrice { get; set; }
    
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDate { get; set; }
    
    public List<BidVm> BidVms { get; set; } = new List<BidVm>();
    public CreateBidVm NewBid { get; set; } 

    public static AuctionDetailsVm FromAuction(Auction auction, string currentUser)
    {
        return new AuctionDetailsVm
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            OwnerName = auction.OwnerName,
            StartingPrice = auction.StartingPrice,
            EndDate = auction.EndDate,
            CanBid = auction.IsActive() && !auction.OwnerName.Equals(currentUser) ,
            HasEnded = !auction.IsActive(),
            NewBid = new CreateBidVm {AuctionId = auction.Id},
            BidVms = auction.Bids.Select(b => BidVm.FromBid(b)).ToList()
        };
    }
}