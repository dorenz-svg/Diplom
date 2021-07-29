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
            var messageTemp = new Messages() { Id = 0, DialogsId = id, Text = message, UserId = userId ,Time=DateTime.UtcNow};
            await context.Messages.AddAsync(messageTemp);
            await context.SaveChangesAsync();
            var messageStatus = new MessageStatus() { Id = 0, IsChecked = false, UserId = userId, MessagesId = messageTemp.Id };
            await context.MessageStatuses.AddAsync(messageStatus);
            await context.SaveChangesAsync();
        }
    }
}
