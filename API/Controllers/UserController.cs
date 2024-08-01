using Business.Abstract;
using Dto.SearchDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SearchUser")]
        public IActionResult GetAllUserAsync(UserSearchDto userSearchDto)
        {
            var response = _userService.SearchAsync(userSearchDto);
            return Ok(response);
        }

        [HttpPost("AddUSer")]
        public async Task<IActionResult> AddUser(UserSearchDto userSearchDto)
        {
            return Ok();
        }
    }
}
