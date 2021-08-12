using Diplom.Models.Entities;
using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFMessageRepository:IMessageRepository
    {
        private readonly DBContext context;
        public EFMessageRepository(DBContext ctx) => context = ctx;

        public async Task Delete(long id,DateTime time)
        {
            var temp = (from x in context.Dialogs.Include(x => x.Messages)
                        from y in x.Messages
                        where x.Id == id && y.Time == time
                        select y).FirstOrDefault();
            context.Messages.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageResponse>> GetMessages(long idDialog, int count)
        {
            var temp = context.Messages.Include(x=>x.User)
                .Where(x => x.DialogsId == idDialog)
                .Skip(count*16)
                .Take(16)
                .Select(x=>new MessageResponse { Id=x.Id,Text=x.Text,Time=x.Time,UserName=x.User.UserName});
            return await Task.FromResult(temp);
        }

        public async Task SetMessage(string message, long id, string userIdSender)
        {
            var messageTemp = new Messages() { Id = 0, DialogsId = id, Text = message, UserId = userIdSender, Time = DateTime.UtcNow };
            context.Messages.Add(messageTemp);
            await context.SaveChangesAsync();
            var users = (from x in context.Users.Include(x => x.Dialogs)
                         from y in x.Dialogs
                         where y.Id == id
                         select x.Id).ToList();
            foreach(var c in users)
                context.MessageStatus.Add(new MessageStatus { Id = 0, IsChecked = false, UserId = c, MessagesId = messageTemp.Id });
            await context.SaveChangesAsync();
        }

        public async Task Update(MessageQuery request)
        {
            var message = (from x in context.Dialogs.Include(x => x.Messages)
                           from y in x.Messages
                           where x.Id == request.Id && y.Time == request.Time
                           select y).FirstOrDefault();
            message.Text = request.Message;
            await context.SaveChangesAsync();
        }
    }
}
