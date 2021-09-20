using Diplom.Infrastructure;
using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Diplom.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository repository;
        private readonly ISaveImage image;
        public MessageController(IMessageRepository repo,ISaveImage img)
        {
            repository = repo;
            image = img;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessages(long idDialog, int count)
        {
            return Ok(await repository.GetMessages(idDialog, count));
        }
        [HttpPost]
        public async Task<ActionResult> CreateMessage([FromForm] MessageQuery query)
        {
            if (query.Message == null && query.File == null)
                return BadRequest();
            if (query.File != null)
                query.PhotosPath = await image.Save(query.File);
            query.IdSender = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await repository.SetMessage(query);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMessage(MessageQuery query)
        {
            query.PhotosPath = await image.Save(query.File);
            await repository.Update(query);
            return Ok();
        }


    }
}
