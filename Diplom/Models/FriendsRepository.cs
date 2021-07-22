using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class FriendsRepository : IFriendsRepository
    {
        private readonly DBContext context;
        public FriendsRepository(DBContext ctx) => context = ctx;

        public async Task AddFriend(string idUser,string idFriend)
        {
            var friend = new Friends() { Id=0,User1Id=idUser,User2Id=idFriend};
            context.Friends.Add(friend);
            await context.SaveChangesAsync();
        }

        public async Task DeleteFriend(string idUser, string idDelete)
        {
            var id = context.Friends.Where(x => x.User1Id == idUser && x.User2Id==idDelete).Select(y => y.Id).FirstOrDefault();
            context.Remove(id);
            await context.SaveChangesAsync();

        }

        public async Task<IEnumerable<User>> GetFriends(string id)
        {
            var friends = context.Friends.Where(x => x.User1Id == id).Select(y => y.User2).ToList();
            return await Task.FromResult(friends);
        }
    }
}
