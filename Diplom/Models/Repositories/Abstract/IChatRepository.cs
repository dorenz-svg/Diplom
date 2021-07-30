using Diplom.Models.Entities;
using Diplom.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IChatRepository
    {
        public Task SetMessage(string message,long id,string userIdSender, string userIdReceiver);
        public Task<long> CreateDialog(string userId1,string userId2,string name);
        public Task<IEnumerable<DialogResponse>> GetDialogs(string id);
        public Task<IEnumerable<UserResponse>> GetUsersDialog(long id);
        public Task DeleteDialog(long id);
    }
}
