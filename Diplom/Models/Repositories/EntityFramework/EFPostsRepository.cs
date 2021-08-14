using Diplom.Models.Entities;
using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFPostsRepository : IPostsRepository
    {
        public readonly DBContext context;
        public EFPostsRepository(DBContext ctx) => context = ctx;
        public async  Task Create(PostQuery query)
        {
            context.Posts.Add( new Posts {Id=0,UserId=query.Id,Text=query.Text,Time=query.Time });
            await context.SaveChangesAsync();
        }

        public async Task Delete(PostQuery query)
        {
            var temp= context.Posts.FirstOrDefault(x => x.UserId == query.Id && x.Time == query.Time);
            context.Posts.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostsResponse>> Get(string id)
        {
            return await Task.FromResult(context.Posts.Where(x => x.UserId == id).Select(x=> new PostsResponse { Text=x.Text,Time=x.Time }));
        }

        public async Task Update(PostQuery query)
        {
            var temp = context.Posts.FirstOrDefault(x => x.UserId == query.Id && x.Time == query.Time);
            temp.Text = query.Text;
            await context.SaveChangesAsync();
        }
    }
}
