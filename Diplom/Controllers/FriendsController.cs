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
        private readonly string idUser;
        public FriendsController(IFriendsRepository repo)
        {
            repository = repo;
            ClaimsPrincipal currentUser = User;
            idUser = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        // GET: FriendsController
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<User>> Index()
        {
            return await repository.GetFriends(idUser);
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
                await repository.AddFriend(idUser, id);
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            else
            {
                await repository.DeleteFriend(idUser, id);
                return Ok();
            }
        }
    }
}
