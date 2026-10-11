using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;


namespace AuctionApp.Persistence;

public class AuctionDB
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(Auction.TitleMaxLength)]
    public string Title { get; set; }

    [Required]
    [MaxLength(Auction.TitleMaxLength*2)] 
    public string Description { get; set; }

    [Required]
    public string OwnerName { get; set; }

    [Required]
    public int StartingPrice { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }

    
    public List<BidDB> BidDbs { get; set; } = new List<BidDB>();
}