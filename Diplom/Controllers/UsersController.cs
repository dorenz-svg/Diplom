using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository repository;
        public UsersController(IUsersRepository repo) => repository = repo;
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Get(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await repository.Get(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Put(MyUser user)
        {
            if(user is null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await repository.Put(user));
            }
            
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            else
            {
                await repository.Delete(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok();
            }
        }
    }
}
