using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Configuration;
using Diplom.Models.Query;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly DBContext context;
        private readonly IConfiguration configuration;
        public EFUsersRepository(DBContext ctx,IConfiguration config)
        { 
            context = ctx;
            configuration = config;
        }

        public async Task<object> AddUserPhoto(string id, string PathPhoto)
        {
            var user = context.Users.Include(x => x.Photos).FirstOrDefault(x => x.Id == id);
            var temp = new Photos { Id = 0, Time = DateTime.UtcNow, Path = PathPhoto };
            context.Photos.Add(temp);
            user.Photos = temp;
            await context.SaveChangesAsync();
            return new { Path = configuration.GetConnectionString("ApplicationUrl") + PathPhoto };
        }

        public async Task Delete(string id)
        {
            var temp = context.Users.Include(x=>x.Friends).FirstOrDefault(x=>x.Id==id);
            context.Users.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task<UserResponse> Get(string id)
        {
            var temp = (from user in context.Users.Include(x => x.Posts).Include(x=>x.Photos)
                        where user.Id == id
                        select new UserResponse { Id = user.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            ProfilePhoto= user.Photos.Path != null? configuration.GetConnectionString("ApplicationUrl") + user.Photos.Path:null,
                            UserPosts= user.Posts.Select(x=>new UserPosts { Id=x.Id,
                                                                            Text=x.Text,
                                                                            PhotosUrl=x.Photos.Select(c=>c.Path).ToList(),
                                                                            Time=x.Time}).Take(20).ToList()}
                        ).FirstOrDefault();
            return await Task.FromResult(temp);
        }

        public async Task<UserResponse> Put(UserQuery user)
        {
            var currentUser =  context.Users.Find(user.Id);
            currentUser.UserName = user.UserName;
            currentUser.PhoneNumber = user.Phone;
            await context.SaveChangesAsync();
            return new UserResponse { Id=currentUser.Id,UserName=currentUser.UserName};
        }
    }
}
