using System.ComponentModel.DataAnnotations;


namespace AuctionApp.Models.Admin;

public class UserVm
{
    [ScaffoldColumn(false)]
    public string Id { get; set; }
    [Display(Name = "Email / Username")]
    public string Email { get; set; }
}