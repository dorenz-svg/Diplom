using System.Threading.Tasks;
using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private readonly DBContext context;
        public EFUsersRepository(DBContext ctx) => context = ctx;
        public async Task Delete(string id)
        {
            context.Users.Remove(new MyUser { Id=id});
            await context.SaveChangesAsync();
        }

        public async Task<UserResponse> Get(string id)
        {
            var user= await context.Users.FindAsync(id);
            return new UserResponse { Id=user.Id,UserName=user.UserName };
        }

        public async Task<UserResponse> Put(MyUser user)
        {
            var currentUser = await context.Users.FindAsync(user.Id);
            currentUser.UserName = user.UserName;
            await context.SaveChangesAsync();
            return new UserResponse { Id=currentUser.Id,UserName=currentUser.UserName};
        }
    }
}
