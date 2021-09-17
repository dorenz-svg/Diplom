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
        [HttpPost]
        public async Task<ActionResult> CreateMessage(MessageQuery query)
        {
            MessageWithPhoto message = new MessageWithPhoto { IdDialog = query.IdDialog, Message = query.Message,IdSender=query.IdSender, Time = query.Time, PhotosPath = new List<string>() };
            await repository.SetMessage(message);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMessage(MessageQuery query)
        {
            MessageWithPhoto message = new MessageWithPhoto { IdDialog = query.IdDialog, Message = query.Message, IdSender = query.IdSender, Time = query.Time, PhotosPath = new List<string>() };
            await repository.Update(message);
            return Ok();
        }
    }
}
