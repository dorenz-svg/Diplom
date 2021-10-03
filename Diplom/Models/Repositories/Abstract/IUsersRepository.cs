using Diplom.Models.Entities;
using Diplom.Models.Query;
using Diplom.Models.Response;
using System;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IUsersRepository
    {
        public Task<UserResponse> Get(string id);
        public Task Delete(string id);
        public Task<UserResponse> Put(UserQuery user);
        public Task<object> AddUserPhoto(string id,string PathPhoto);
    }
}
