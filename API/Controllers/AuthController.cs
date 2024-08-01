using Business.Abstract;
using Business.Constants;
using Dto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto dto)
        {
            var userToLogin = _authService.Login(dto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = _authService.CreateAccessToken(userToLogin.Data, userToLogin.Data.ID);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(Messages.LoginError);
        }
      
    }
}
