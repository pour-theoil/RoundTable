﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoundTable.Models;
using RoundTable.Models.ViewModels;
using RoundTable.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoundTable.Controllers
{
    public class StoryController : Controller
    {
        private readonly IReporterRepository _reporterRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStoryTypeRepository _storyTypeRepository;
        private readonly INationalOutletRepostitory _nationalOutletRepostitory;
        private readonly IStatusRepository _statusRepository;

        public StoryController( IReporterRepository reporterRepository, 
                                IStoryRepository storyRepository, 
                                IStoryTypeRepository storyTypeRepository,
                                ICategoryRepository categoryRepository,
                                INationalOutletRepostitory nationalOutlet,
                                IStatusRepository statusRepository
                               )
        {
            _reporterRepository = reporterRepository;
            _storyRepository = storyRepository;
            _storyTypeRepository = storyTypeRepository;
            _categoryRepository = categoryRepository;
            _nationalOutletRepostitory = nationalOutlet;
            _statusRepository = statusRepository;
        }

        // GET: StoryController
        public ActionResult Index()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var stories = _storyRepository.GetAll(int.Parse(firebaseUserId));
            return View(stories);
        }

        // GET: StoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoryController/Create
        public ActionResult Create()
        {
            var types = _storyTypeRepository.GetAllStoryType();
            var category = _categoryRepository.GetAllCategory();
            var national = _nationalOutletRepostitory.GetAllNationalOutlet();
            var status = _statusRepository.GetAllStatus();
            var vm = new AddStoryViewModel()
            {
                status = status,
                story = new Story(),
                storyTypes = types,
                categories = category,
                nationalOutlets = national
            };
            return View(vm);
        }

        // POST: StoryController/Create
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

        // GET: StoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoryController/Edit/5
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

        // GET: StoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        private Reporter GetCurrentReporterProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (firebaseUserId != null)
            {
                var user = _reporterRepository.GetByFirebaseUserId(firebaseUserId);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
