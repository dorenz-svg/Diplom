using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFFriendsRepository : IFriendsRepository
    {
        private readonly DBContext context;
        public EFFriendsRepository(DBContext ctx) => context = ctx;

        public async Task AddFriend(string idUser,string idFriend)
        {
            var friend = new Friends() { Id=0,User1Id=idUser,User2Id=idFriend};
            context.Friends.Add(friend);
            await context.SaveChangesAsync();
        }

        public async Task DeleteFriend(string idUser, string idDelete)
        {
            var id = context.Friends.Where(x => x.User1Id == idUser && x.User2Id==idDelete).FirstOrDefault();
            context.Friends.Remove(id);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<FriendsResponse>> GetFriends(string id)
        {
            var friends = context.Friends.Where(x => x.User1Id == id).Select(y => y.User2).Select(x=> new FriendsResponse{ Id=x.Id,UserName=x.UserName}).ToList();
            return await Task.FromResult(friends);
        }
    }
}
