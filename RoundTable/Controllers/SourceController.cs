using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoundTable.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoundTable.Controllers
{
    public class SourceController : Controller
    {
        private readonly ISourceRepository _sourceRepository;

        public SourceController(ISourceRepository sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }
        // GET: SourceController
        public ActionResult Index()
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var sources = _sourceRepository.GetAllSouces(firebaseUserId);
            return View(sources);
        }

        // GET: SourceController/Details/5
        public ActionResult Details(int id)
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var source = _sourceRepository.GetSouceById(id, firebaseUserId);
            return View(source);
        }

        // GET: SourceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SourceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SourceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SourceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SourceController/Delete/5
        public ActionResult Delete(int id)
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var source = _sourceRepository.GetSouceById(id, firebaseUserId);
            return View(source);
        }

        // POST: SourceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _sourceRepository.DeleteSource(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
