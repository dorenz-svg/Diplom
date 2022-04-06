using Diplom.Models.Entities;
using Diplom.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IDialogRepository
    {
        public Task CreateDialog(string userId1,string userName,string name);
        public Task<IEnumerable<DialogResponse>> GetDialogs(string id);
        public Task<IEnumerable<UserResponse>> GetUsersDialog(long id);
        public Task UpdateDialog(long id,string name);
        public Task DeleteDialog(long id);
    }
}
