using Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        internal UserContext GetCurrentUser()
        {
            var userContext = new UserContext
            {
                SystemUserId = Convert.ToInt32(User?.Claims?.FirstOrDefault(f => f.Type == "currentUserId")?.Value),
                FullName = User?.Claims?.FirstOrDefault(f => f.Type == "fullName")?.Value,
                Email = User?.Claims?.FirstOrDefault(f => f.Type == "systemUserEmail")?.Value,
                Roles = string.IsNullOrEmpty(User?.Claims?.FirstOrDefault(f => f.Type == "systemUserRoles")?.Value) ? new List<string>() : User?.Claims?.FirstOrDefault(f => f.Type == "systemUserRoles")?.Value.Split(',').ToList(),

            };
            return userContext;
        }
    }
}
