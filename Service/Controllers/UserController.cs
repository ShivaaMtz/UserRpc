using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Services;
using System.Net;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManagement _userManagement;

        public UserController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _userManagement.Get(id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RequestDto request)
        {
            var result = await _userManagement.SignUp(request);

            return StatusCode((int)HttpStatusCode.Created, result);
        }

    }
}
