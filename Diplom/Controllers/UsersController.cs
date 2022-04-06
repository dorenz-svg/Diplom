using Diplom.Infrastructure;
using Diplom.Models.Entities;
using Diplom.Models.Query;
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
        public async Task<ActionResult<object>> Get(string id)
        {
            if (id is null)
            {
                var temp = await repository.Get(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                temp.ProfilePhoto = "https://avatars.mds.yandex.net/get-zen_doc/1866022/pub_5cc558ba536f2100b323e4e3_5cc55bee15df6000b3c20fae/scale_1200";
                return Ok(temp);
            }
            else
            {
                return Ok(await repository.Get(id));
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<ActionResult<UserResponse>> Put(UserQuery user)
        {
            user.Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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
        [HttpPost("photo")]
        [Authorize]
        public async Task<ActionResult<object>> UpdateUserPhoto(IFormFile file)
        {
            var temp=await image.Save(file as IFormFileCollection);
            return Ok(await repository.AddUserPhoto(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,temp.FirstOrDefault()));
        }
    }
}
