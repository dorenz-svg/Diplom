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

        public async Task Delete(long id)
        {
            var temp = context.Messages.Include(x => x.MessageStatus).FirstOrDefault(x=>x.Id==id);
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

        public async Task SetMessage(string message, long id, string userIdSender, string userIdReceiver)
        {
            var messageTemp = new Messages() { Id = 0, DialogsId = id, Text = message, UserId = userIdSender, Time = DateTime.UtcNow };
            context.Messages.Add(messageTemp);
            await context.SaveChangesAsync();
            context.MessageStatus.Add(new MessageStatus { Id = 0, IsChecked = false, UserId = userIdReceiver, MessagesId = messageTemp.Id });
            await context.SaveChangesAsync();
        }

        public async Task Update(MessageQuery request)
        {
            var temp = context.Messages.Find(request.Id);
            temp.Text = request.Message;
            await context.SaveChangesAsync();
        }
    }
}
