using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApp.Persistence;

public class BidDB
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string BidderName { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    public DateTime TimePlaced { get; set; }

    [ForeignKey("AuctionId")]
    public AuctionDB AuctionDb { get; set; }

    public int AuctionId { get; set; }
}