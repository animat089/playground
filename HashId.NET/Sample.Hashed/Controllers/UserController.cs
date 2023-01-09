using Microsoft.AspNetCore.Mvc;
using Sample.Hashed.Services.Contracts;

namespace Sample.Hashed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get([FromRoute] string id)
        {
            var result = userService.GetUserById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAll()
        {
            return Ok(userService.GetAllUsers());
        }
    }
}