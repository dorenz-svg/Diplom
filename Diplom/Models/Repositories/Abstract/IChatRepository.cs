using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.Abstract
{
    public interface IChatRepository
    {
        public Task SetMessage(string message,long id, string userId);
    }
}
