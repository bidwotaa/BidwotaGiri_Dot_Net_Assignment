using BidwotaGiri_Dot_Net_Assignment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BidwotaGiri_Dot_Net_Assignment.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<BidwotaGiri_Dot_Net_AssignmentUser> _userManager;
        private readonly SignInManager<BidwotaGiri_Dot_Net_AssignmentUser> _signInManager;

        public AccountController(UserManager<BidwotaGiri_Dot_Net_AssignmentUser> userManager,
                                 SignInManager<BidwotaGiri_Dot_Net_AssignmentUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            return View(user);
        }
    }
}
