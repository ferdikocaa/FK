using Business.Abstract;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityController : BaseController
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost("AddActivity")]
        public IActionResult AddActivity(ActivityDto activityDto)
        {
            var response = _activityService.Add(activityDto,GetCurrentUser());
            return Ok(response);
        }

        [HttpPost("GetAllActivity")]
        public IActionResult GetAllActivity()
        {
            var response = _activityService.GetAll();
            return Ok(response);
        }

        [HttpPost("GetAllWithFilter")]
        public IActionResult GetAllWithFilter()
        {
            var response = _activityService.GetAllWithFilter(null);
            return Ok(response);
        }
    }
}
