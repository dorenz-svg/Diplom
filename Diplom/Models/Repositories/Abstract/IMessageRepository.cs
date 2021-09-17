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
        public Task SetMessage(MessageWithPhoto message);
        public Task<IEnumerable<MessageResponse>> GetMessages(long idDialog, int count);
        public Task Update(MessageWithPhoto request);
        public Task Delete(long id);
        public Task CheckMessages(string idUser,long idDialog);
    }
}
