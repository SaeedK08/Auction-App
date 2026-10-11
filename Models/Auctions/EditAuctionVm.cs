using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Models.Auctions;

public class EditAuctionVm
{
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }
    
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(1024, ErrorMessage = "Description cannot exceed 1024 characters.")]
    [Display(Name = "Description")]
    [DataType(DataType.MultilineText)] // Tells the view to render a <textarea>
    public string Description { get; set; }
}