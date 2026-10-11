using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class BidVm
{
    [Display(Name = "Bidder Name")]
    public string BidderName { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:C0}")]
    public int Amount { get; set; }
    
    [Display(Name = "Time Placed")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")] 
    public DateTime TimePlaced { get; set; }

    public static BidVm FromBid(Bid bid)
    {
        return new BidVm
        {
            BidderName = bid.BidderName,
            Amount = bid.Amount,
            TimePlaced = bid.TimePlaced
        };
    }
}