using Diplom.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IFriendsRepository
    {
        public Task DeleteFriend(string idUser,string idDelete);
        public Task<IEnumerable<FriendsResponse>> GetFriends(string id);
        public Task AddFriend(string idUser, string idFriend);
    }
}
