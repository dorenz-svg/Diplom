using Diplom.Models.Entities;
using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFPostsRepository : IPostsRepository
    {
        private readonly DBContext context;
        private readonly IConfiguration configuration;
        public EFPostsRepository(DBContext ctx, IConfiguration config)
        {
            context = ctx;
            configuration = config;
        }
        public async Task Create(PostQuery query)
        {
            var post = new Posts { Id = 0, UserId = query.Id, Text = query.Text, Time = query.Time };
            context.Posts.Add(post);
            foreach (var c in query.PhotosPath)
            {
                post.Photos.Add(new Photos { Id = 0, Path = c, Time = query.Time });
            }
            await context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var temp = context.Posts.Include(x => x.Photos).Include(x => x.Likes).FirstOrDefault(x => x.Id == id);
            context.Posts.Remove(temp);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<PostsResponse>> Get(string id, int count)
        {
            return await Task.FromResult(context.Posts.Include(x => x.Photos)
                .Where(x => x.UserId == id)
                .Skip(20 * count)
                .Take(20)
                .Select(x => new PostsResponse { Id = x.Id, Text = x.Text, Time = x.Time,IsLike= context.Likes.Any(c=>c.PostId==x.Id && c.UserId==id), Likes = context.Likes.Count(c => c.PostId == x.Id), Path = x.Photos.Select(c => configuration.GetConnectionString("ApplicationUrl") + c.Path) }));
        }

        public async Task Like(long idPost, string idUser, bool like)
        {
            if (like)
                context.Likes.Add(new Likes { PostId = idPost, UserId = idUser });
            else
                context.Likes.Remove(context.Likes.Where(x=>x.PostId==idPost && x.UserId== idUser).FirstOrDefault());
            await context.SaveChangesAsync();
        }

        public async Task Update(PostQuery query)
        {
            var temp = context.Posts.Include(x => x.Photos).FirstOrDefault(x => x.UserId == query.Id && x.Time == query.Time);
            temp.Text = query.Text;
            var pathes = temp.Photos.Select(x => x.Path).ToList();
            var deletePhotosPath = temp.Photos.Select(x => x.Path).Except(query.PhotosPath).ToList();
            var deletePhotos = (from x in temp.Photos
                                from y in deletePhotosPath
                                where x.Path == y
                                select x).ToList();
            foreach (var c in deletePhotos)
                temp.Photos.Remove(c);
            foreach (var c in query.PhotosPath)
            {
                if (!pathes.Contains(c))
                {
                    var photo = new Photos { Id = 0, Path = c, Time = query.Time, PostId = temp.Id };
                    context.Photos.Add(photo);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
