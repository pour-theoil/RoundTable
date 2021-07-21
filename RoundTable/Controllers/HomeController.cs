using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoundTable.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RoundTable.Repositories;

namespace RoundTable.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IReporterRepository _userProfileRepository;

        public HomeController(IReporterRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public IActionResult Index()
        {
            var userProfileId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userProfile = _userProfileRepository.GetById(userProfileId);
            return View(userProfile);
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
