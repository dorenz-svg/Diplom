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
        
        [HttpPost]
        public async Task<ActionResult> CreateDialog([FromBody] DialogsQuery query)
        {
            await repository.CreateDialog(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, query.UserName, query.DialogName);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<DialogResponse>> GetDialogs()
        {
            return await repository.GetDialogs(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        /// <summary>
        /// Получить всех пользователей диалога
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("users")]
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
