using System.Linq;
using System.Threading.Tasks;
using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly DBContext context;
        public EFUsersRepository(DBContext ctx) => context = ctx;
        public async Task Delete(string id)
        {
            var temp = context.Users.Include(x=>x.Friends).FirstOrDefault(x=>x.Id==id);
            context.Users.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task<UserResponse> Get(string id)
        {
            var user=  context.Users.Find(id);
            return await Task.FromResult(new UserResponse { Id=user.Id,UserName=user.UserName });
        }

        public async Task<UserResponse> Put(MyUser user)
        {
            var currentUser =  context.Users.Find(user.Id);
            currentUser.UserName = user.UserName;
            await context.SaveChangesAsync();
            return new UserResponse { Id=currentUser.Id,UserName=currentUser.UserName};
        }
    }
}
