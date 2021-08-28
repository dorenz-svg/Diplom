using Diplom.Models.Entities;
using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class DialogsController:ControllerBase
    {
        private readonly IDialogRepository repository;
        public DialogsController(IDialogRepository repo) => repository = repo;
        
        [HttpPost("create")]
        public async Task<ActionResult> CreateDialog([FromBody] DialogsQuery query)
        {
            await repository.CreateDialog(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, query.UserId, query.Name);
            return Ok();
        }
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<DialogResponse>>> GetDialogs()
        {
            return Ok(await repository.GetDialogs(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
        }
        [HttpGet("getusers")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersDialogs(long id)
        {
            return Ok(await repository.GetUsersDialog(id));
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteDialog(long id)
        {
            await repository.DeleteDialog(id);
            return Ok();
        }
    }
}
