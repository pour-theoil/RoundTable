using Microsoft.AspNetCore.Mvc;
using RoundTable.Models;
using RoundTable.Repositories;
using System;
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

        public IActionResult Edit(int id)
        {
            var reporter = _reporterRepository.GetById(id);
            return View(reporter);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Reporter reporter)
        {
            try
            {
               
                _reporterRepository.Update(reporter);
                return RedirectToAction("Details", new { id = reporter.Id });
            }
            catch (Exception ex)
            {

        

                return View(reporter);
            }
        }
    }
}
