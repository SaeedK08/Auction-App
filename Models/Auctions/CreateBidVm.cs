using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models.Auctions;

public class CreateBidVm
{
    [ScaffoldColumn(false)]
    public int AuctionId { get; set; }

    [Required(ErrorMessage = "You must enter an amount.")]
    [Range(1, int.MaxValue, ErrorMessage = "Bid amount must be greater than zero.")]
    public int Amount { get; set; }
}