using System.Data;
using AuctionApp.Core.Interfaces;
using AuctionApp.Models.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Controllers
{
    [Authorize]
    public class AuctionsController : Controller
    {
        
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        // GET: AuctionController
        public ActionResult Index()
        {
            var auctions = _auctionService.GetAllActiveAuctions();
            List<AuctionVm> auctionListVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionListVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionListVms);
        }

        // GET: AuctionController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var auction = _auctionService.GetAuctionById(id); 
                AuctionDetailsVm vm = AuctionDetailsVm.FromAuction(auction, User.Identity.Name);   
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Auction was not found, or a database error accured";
                return RedirectToAction("Index");
            }
        }

        // GET: AuctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateAuctionVm vm)
        {
            if (!ModelState.IsValid)
            { 
                return View(vm);
            }

            try
            {
                _auctionService.CreateAuction(
                    vm.Title,
                    vm.Description,
                    User.Identity.Name,
                    vm.StartingPrice,
                    vm.EndDate);
                
                TempData["SuccessMessage"] = "Auction was successfully created!";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            catch (DataException e)
            {
                ModelState.AddModelError(string.Empty, 
                    "A database error occurred while creating the auction.");
                return View(vm);
            }
        }

        // GET: AuctionController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var auction = _auctionService.GetAuctionById(id);
                if(!auction.OwnerName.Equals(User.Identity.Name)) 
                    throw new UnauthorizedAccessException("Only the owner is allowed to edit this auction.");
                
                EditAuctionVm vm = new EditAuctionVm()
                {
                    Id = auction.Id,
                    Title = auction.Title,
                    Description = auction.Description,
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AuctionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string description)
        {
            try
            {
                _auctionService.UpdateDescription(id, description, User.Identity.Name);
                TempData["SuccessMessage"] = "Description was successfully updated!";
                return RedirectToAction(nameof(Index));
            }
            catch (UnauthorizedAccessException ex)
            {
                TempData["EditError"] = ex.Message;
            }
            catch (ArgumentException ex)
            {
                TempData["EditError"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["EditError"] = ex.Message;
            }
            return RedirectToAction("Edit", new { id = id});
        }
        
        // POST: AuctionsController/PlaceBid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceBid(CreateBidVm vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["BidError"] = "Enter a valid amount.";
                return RedirectToAction(nameof(Details), new { vm.AuctionId });
            }

            try
            {
                _auctionService.PlaceBid(vm.AuctionId, User.Identity.Name, vm.Amount);
                TempData["BidSuccess"] = "Your bid has been placed.";
            }
            catch (KeyNotFoundException)
            {
                TempData["ErrorMessage"] = "Auction was not found, or a database error accured";
                return RedirectToAction("Index");
            }
            catch (Exception e) when (e is InvalidOperationException or ArgumentException)
            {
                TempData["BidError"] = e.Message;                    
            }

            return RedirectToAction(nameof(Details), new { id = vm.AuctionId });
        }
        
        
        // GET: AuctionsController/MyBids
        public ActionResult MyBids()
        {
            var auctions = _auctionService.GetOngoingAuctionsByBidder(User.Identity.Name);
    
            List<AuctionVm> auctionListVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionListVms.Add(AuctionVm.FromAuction(auction));
            }
    
            return View(auctionListVms);
        }

        // GET: AuctionsController/WonAuctions
        public ActionResult WonAuctions()
        {
            var auctions = _auctionService.GetWonAuctionsBy(User.Identity.Name);
    
            List<AuctionVm> auctionListVms = new List<AuctionVm>();
            foreach (var auction in auctions)
            {
                auctionListVms.Add(AuctionVm.FromAuction(auction));
            }
    
            return View(auctionListVms);
        }
    }
}
