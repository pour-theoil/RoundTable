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
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ChartsController(IStoryRepository storyRepository,
                                IStatusRepository statusRepository,
                                ICategoryRepository categoryRepository)
        {
            _storyRepository = storyRepository;
            _statusRepository = statusRepository;
            _categoryRepository = categoryRepository;
        }
        [HttpGet("Status")]
        public IActionResult GetStatus()
        {
            var chartvalues = new List<StatusPieChart>();
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var statuses = _statusRepository.GetAllStatus();

            foreach (var status in statuses)
            {
                var value = new StatusPieChart()
                {
                    y = _storyRepository.StoryStatusCount(status.Id, firebaseUserId),
                    label = status.Name
                };
                if (value.y > 0)
                {
                    chartvalues.Add(value);
                }
            }
            return Ok(chartvalues);
        }

        [HttpGet("Mix")]
        public IActionResult GetCategory()
        {
            var categoryvalues = new List<StatusPieChart>();
            var firebaseUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var categories = _categoryRepository.GetAllCategory();

            foreach (var category in categories)
            {
                var value = new StatusPieChart()
                {
                    y = _storyRepository.StoryStatusCount(category.Id, firebaseUserId),
                    label = category.Name
                };
                if (value.y > 0)
                {
                    categoryvalues.Add(value);

                }
            }
            return Ok(categoryvalues);
        }
    }

}
