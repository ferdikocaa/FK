using Business.Abstract;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivityTypeController : BaseController
    {
        private readonly IActivityTypeService _activityTypeService;
        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        [HttpPost("AddActivityType")]
        public IActionResult AddActivityType(ActivityTypeDto activityTypeDto)
        {
            var response = _activityTypeService.Add(activityTypeDto, GetCurrentUser());
            return Ok();
        }

        [HttpPost("GetAllActivityType")]
        public IActionResult GetAllActivityType()
        {
            try
            {
                var response = _activityTypeService.GetAll();
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("GetAllWithFilterActivityType")]
        public IActionResult GetAllWithFilterActivityType()
        {
            var response = _activityTypeService.GetAllWithFilter(x => x.Name == "Test");
            return Ok(response);
        }
    }
}
