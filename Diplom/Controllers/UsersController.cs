using Diplom.Infrastructure;
using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        private readonly ISaveImage image;
        public UsersController(IUsersRepository repo,ISaveImage img) 
        { 
            repository = repo;
            image = img;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Get(string id)
        {
            if (id is null)
            {
                return Ok(await repository.Get(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            }
            else
            {
                return Ok(await repository.Get(id));
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Put(MyUser user)
        {
            if (user is null)
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
        public async Task<ActionResult> Delete()
        {
            await repository.Delete(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<object>> UpdateUserPhoto(IFormFile file)
        {
            var temp=await image.Save(file as IFormFileCollection);
            return Ok(await repository.AddUserPhoto(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,temp.FirstOrDefault()));
        }
    }
}
