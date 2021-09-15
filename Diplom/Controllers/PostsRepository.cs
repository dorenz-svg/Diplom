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
    public class PostsRepository:ControllerBase
    {
        public readonly IPostsRepository repository;
        public PostsRepository(IPostsRepository repo) => repository = repo;
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
            await repository.Update(query);
            return Ok();
        }
    }
}
