using Microsoft.AspNetCore.Http;
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
        private readonly ISourceRepository _sourceRepository;

        public StoryController(IReporterRepository reporterRepository,
                                IStoryRepository storyRepository,
                                IStoryTypeRepository storyTypeRepository,
                                ICategoryRepository categoryRepository,
                                INationalOutletRepostitory nationalOutlet,
                                IStatusRepository statusRepository,
                                ISourceRepository sourceRepository)
        {
            _reporterRepository = reporterRepository;
            _storyRepository = storyRepository;
            _storyTypeRepository = storyTypeRepository;
            _categoryRepository = categoryRepository;
            _nationalOutletRepostitory = nationalOutlet;
            _statusRepository = statusRepository;
            _sourceRepository = sourceRepository;
        }

        // GET: StoryController
        public IActionResult Index(string searchString, bool IsChecked)
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var story = _storyRepository.GetAll(int.Parse(firebaseUserId));
            var stories = from s in story
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                stories = stories.Where(s => s.Slug.Contains(searchString));
            }

            if (IsChecked)
            {
                stories = stories.Where(s => s.Status.Id == 7);
            }
            return View(stories);
        }

        // GET: StoryController/Details/5
        public ActionResult Details(int id)
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var story = _storyRepository.GetStoryById(id, firebaseUserId);
            return View(story);
        }

        // GET: StoryController/Create
        public ActionResult Create()
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var types = _storyTypeRepository.GetAllStoryType();
            var category = _categoryRepository.GetAllCategory();
            var national = _nationalOutletRepostitory.GetAllNationalOutlet();
            var status = _statusRepository.GetAllStatus();
            var sources = _sourceRepository.GetAllSouces(firebaseUserId);
            var vm = new AddStoryViewModel()
            {
                status = status,
                story = new Story(),
                storyTypes = types,
                categories = category,
                nationalOutlets = national,
                Sources = sources

            };
            return View(vm);
        }

        // POST: StoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddStoryViewModel vm)
        {
            try
            {
                vm.story.ReporterId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _storyRepository.AddStory(vm.story);

                if (vm.SelectedValues != null)
                {

                    foreach (var value in vm.SelectedValues)
                    {
                        _sourceRepository.AddSourceToStory(vm.story.Id, value);
                    }
                }
                return RedirectToAction("Details", new { id = vm.story.Id });
            }
            catch
            {

                var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                vm.status = _statusRepository.GetAllStatus();
                vm.storyTypes = _storyTypeRepository.GetAllStoryType();
                vm.categories = _categoryRepository.GetAllCategory();
                vm.nationalOutlets = _nationalOutletRepostitory.GetAllNationalOutlet();
                vm.Sources = _sourceRepository.GetAllSouces(firebaseUserId);
                return View(vm);
            }
        }

        // GET: StoryController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {

                var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var types = _storyTypeRepository.GetAllStoryType();
                var category = _categoryRepository.GetAllCategory();
                var national = _nationalOutletRepostitory.GetAllNationalOutlet();
                var status = _statusRepository.GetAllStatus();
                var sources = _sourceRepository.GetAllSouces(firebaseUserId);
                var vm = new AddStoryViewModel()
                {
                    status = status,
                    story = _storyRepository.GetStoryById(id, firebaseUserId),
                    storyTypes = types,
                    categories = category,
                    nationalOutlets = national,
                    Sources = sources

                };
                return View(vm);
            }
            catch
            {

                return NotFound();
            }
        }

        // POST: StoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AddStoryViewModel vm)
        {
            try
            {
                vm.story.Sources = new List<Source>();
                if (vm.SelectedValues != null)
                {

                    foreach (var value in vm.SelectedValues)
                    {
                        var source = new Source()
                        {
                            Id = value,
                        };
                        vm.story.Sources.Add(source);
                    }
                }
                _storyRepository.UpdateStory(vm.story);
                return RedirectToAction("Details", new { id = vm.story.Id });
            }
            catch (Exception ex)
            {
                var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                vm.storyTypes = _storyTypeRepository.GetAllStoryType();
                vm.categories = _categoryRepository.GetAllCategory();
                vm.nationalOutlets = _nationalOutletRepostitory.GetAllNationalOutlet();
                vm.status = _statusRepository.GetAllStatus();
                vm.Sources = _sourceRepository.GetAllSouces(firebaseUserId);


                return View(vm);
            }
        }

        // GET: StoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Story story = _storyRepository.GetStoryById(id, firebaseUserId);
            return View(story);
        }

        // POST: StoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _storyRepository.DeleteStory(id);
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
