using Diplom.Models.Query;
using Diplom.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IMessageRepository
    {
        public Task SetMessage(string message, long id, string userIdSender);
        public Task<IEnumerable<MessageResponse>> GetMessages(long idDialog, int count);
        public Task Update(MessageQuery request);
        public Task Delete(long id, DateTime time);
        public Task CheckMessages(string idUser,long idDialog);
    }
}
