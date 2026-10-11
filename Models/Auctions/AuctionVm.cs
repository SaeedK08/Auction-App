using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Models.Auctions;

public class AuctionVm
{
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }

    [Display(Name = "Titel")]
    public string Title { get; set; }

    [Display(Name = "Description")]
    public string Description { get; set; }

    [Display(Name = "Seller")]
    public string OwnerName { get; set; }

    [Display(Name = "Staring Price")]
    [DisplayFormat(DataFormatString = "{0:C0}")]
    public int StartingPrice { get; set; }

    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")] 
    public DateTime EndDate { get; set; }
    
    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            OwnerName = auction.OwnerName,
            StartingPrice = auction.StartingPrice,
            EndDate = auction.EndDate
        };
    }
    
}