using System.Diagnostics;
using BidwotaGiri_Dot_Net_Assignment.Areas.Identity.Data;
using BidwotaGiri_Dot_Net_Assignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BidwotaGiri_Dot_Net_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<BidwotaGiri_Dot_Net_AssignmentUser> _userManager;

        public HomeController(ILogger<HomeController> logger,UserManager<BidwotaGiri_Dot_Net_AssignmentUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
           ViewData["Username"]= _userManager.GetUserName(this.User);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
