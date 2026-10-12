using AuctionApp.Areas.Identity.Data;
using AuctionApp.Core.Interfaces;
using AuctionApp.Models.Admin;
using AuctionApp.Models.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly IAuctionService _auctionService;

    public AdminController(UserManager<AppIdentityUser> userManager,  IAuctionService auctionService)
    {
        _userManager = userManager;
        _auctionService = auctionService;
    }

    public IActionResult Index()
    {
        List<UserVm> users = _userManager.Users.Select(user => new UserVm
        {
            Id = user.Id,
            Email = user.Email,
        }).ToList();
        return View(users);
    }

    
    // GET UserAuctions/email
    public IActionResult UserAuctions(string email)
    {
        List<AuctionVm> auctionVms = _auctionService.GetAuctionsByOwnerName(email)
            .Select(a => AuctionVm.FromAuction(a))
            .ToList();
        
        ViewBag.UserEmail = email;
        return View(auctionVms);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAuction(int id, string ownerEmail)
    {
        try
        {
            _auctionService.DeleteAuction(id);
            TempData["DeleteSuccess"] = "Auction was successfully deleted.";
        }
        catch (Exception ex)
        {
            TempData["DeleteError"] = "Failed to delete the auction. " + ex.Message;
        }

        
        return RedirectToAction("UserAuctions", new { email = ownerEmail });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUser(string email)
    {
        if (email == User.Identity.Name)
        {
            TempData["ErrorMessage"] = "You cannot delete your own admin account.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            _auctionService.DeleteAllDataForUser(email);
            
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"User {email} and all their resources were successfully deleted.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Domain data was deleted, but failed to delete user from Identity.";
                }
            }
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "An error occurred while deleting the user's resources." + e.Message;
        }

        return RedirectToAction(nameof(Index));
    }
}