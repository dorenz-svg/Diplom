using Diplom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsRepository repository;
        public FriendsController(IFriendsRepository repo)
        {
            repository = repo;
        }
        // GET: FriendsController
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<MyUser>> Index()
        {
            return await repository.GetFriends(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        // POST: FriendsController/Create
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                await repository.AddFriend(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, id);
                return Ok();
            }
        }
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                await repository.DeleteFriend(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, id);
                return Ok();
            }
        }
    }
}
