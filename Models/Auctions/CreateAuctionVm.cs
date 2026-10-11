using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models.Auctions;

public class CreateAuctionVm
{
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(128, ErrorMessage = "Title cannot exceed 128 characters.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(1024, ErrorMessage = "Description cannot exceed 1024 characters.")]
    public string Description { get; set; }
    

    [Required(ErrorMessage = "Starting price is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Price must be strictly positive.")]
    [Display(Name = "Starting Price")]
    public int StartingPrice { get; set; }

    [Required(ErrorMessage = "End date is required.")]
    [Display(Name = "End Date")]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);
}