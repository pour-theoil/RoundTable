using Microsoft.AspNetCore.Mvc;
using RoundTable.Repositories;
using System.Security.Claims;

namespace RoundTable.Controllers
{
    public class ReporterController : Controller
    {
        private readonly IReporterRepository _reporterRepository;
        
        public ReporterController(IReporterRepository reporterRepository)
        {
            _reporterRepository = reporterRepository;
        }

        public IActionResult Details()
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            return View(_reporterRepository.GetById(firebaseUserId));
        }
    }
}
