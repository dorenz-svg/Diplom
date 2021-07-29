using Diplom.Models.Entities;
using Diplom.Models.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFChatRepository : IChatRepository
    {
        private readonly DBContext context;
        public EFChatRepository(DBContext ctx) => context = ctx;

        public async Task SetMessage(string message, long id,string userId)
        {
            context.Messages.Add(new Messages{ Id = 0, DialogsId = id, Text = message, UserId = userId ,MessageStatus= new MessageStatus { Id = 0, IsChecked = false, UserId = userId } });
            await context.SaveChangesAsync();
        }
    }
}
