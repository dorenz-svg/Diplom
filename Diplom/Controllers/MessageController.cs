using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController:ControllerBase
    {
        private readonly IMessageRepository repository;
        public MessageController(IMessageRepository repo) => repository = repo;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessages(long idDialog,int count)
        {
            return Ok(await repository.GetMessages(idDialog, count));
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMessage(MessageQuery query)
        {
            await repository.Update(query);
            return Ok ();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteMessage(long id)
        {
            await repository.Delete(id);
            return Ok();
        }
    }
}
