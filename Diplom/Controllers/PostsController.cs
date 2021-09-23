using Diplom.Infrastructure;
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
    public class PostsController:ControllerBase
    {
        private readonly IPostsRepository repository;
        private readonly ISaveImage image;
        public PostsController(IPostsRepository repo,ISaveImage img)
        {
            repository = repo;
            image = img;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostsResponse>>> Get(string id,int count=1)
        {
            if (id is null)
                return Ok(await repository.Get(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, count));
            else
                return Ok(await repository.Get(id,count));
        }
        [HttpPost]
        public async Task<ActionResult> Create(PostQuery query)
        {
            if (query.Text is null && query.File is null)
                return BadRequest();
            if(query.File !=null)
                query.PhotosPath = await image.Save(query.File);
            await repository.Create(query);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            await repository.Delete(id);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> Update(PostQuery query)
        {
            query.PhotosPath = await image.Save(query.File);
            await repository.Update(query);
            return Ok();
        }
        [HttpPost("like")]
        public async Task<ActionResult> Like(long idPost, bool like)
        {
            await repository.Like(idPost, User.FindFirst(ClaimTypes.NameIdentifier)?.Value, like);
            return Ok();
        }
    }
}
