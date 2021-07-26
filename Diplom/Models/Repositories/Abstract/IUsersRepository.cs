using Diplom.Models.Entities;
using Diplom.Models.Response;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IUsersRepository
    {
        public Task<UserResponse> Get(string id);
        public Task Delete(string id);
        public Task<UserResponse> Put(MyUser user);
    }
}
