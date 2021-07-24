using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public interface IFriendsRepository
    {
        public Task DeleteFriend(string idUser,string idDelete);
        public Task<IEnumerable<MyUser>> GetFriends(string id);
        public Task AddFriend(string idUser, string idFriend);
    }
}
