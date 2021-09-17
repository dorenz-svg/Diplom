using Diplom.Models.Entities;
using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Diplom.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Models.Repositories.EntityFramework
{
    public class EFMessageRepository:IMessageRepository
    {
        private readonly DBContext context;
        public EFMessageRepository(DBContext ctx) => context = ctx;

        public async Task CheckMessages(string idUser, long idDialog)
        {
            var temp = context.MessageStatus
                .Include(x => x.Messages)
                .ThenInclude(x => x.Dialogs)
                .Where(x => !x.IsChecked && x.UserId == idUser && x.Messages.DialogsId == idDialog).ToList();
            foreach (var c in temp)
                c.IsChecked = true;
            await context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var temp = context.Messages.Include(x=>x.MessageStatus).FirstOrDefault(x=>x.Id==id);
            context.Messages.Remove(temp);
            await context.SaveChangesAsync();
        }
        //нужно будет добавить путь ресурса к фото
        public async Task<IEnumerable<MessageResponse>> GetMessages(long idDialog, int count)
        {
            var message = context.Messages.Where(x => x.DialogsId == idDialog)
                .Include(x => x.User)
                .Skip(count * 16)
                .Take(16)
                .Select(x => new MessageResponse
                {
                    Id = x.Id,
                    Text = x.Text,
                    Time = x.Time,
                    UserName = x.User.UserName,
                    IsChecked = context.MessageStatus.Include(c => c.User)
                    .Where(c => c.MessagesId == x.Id && c.UserId == x.UserId)
                    .Select(c => c.IsChecked).FirstOrDefault(),
                    Path= x.Photos.Select(x=>x.Path)
                });
            return await Task.FromResult(message);
        }

        public async Task SetMessage(MessageWithPhoto message)
        {
            var messageTemp = new Messages() { Id = 0, DialogsId = message.IdDialog, Text = message.Message, UserId = message.IdSender, Time = DateTime.UtcNow };
            context.Messages.Add(messageTemp);
            var users = (from x in context.Users.Include(x => x.Dialogs)
                         from y in x.Dialogs
                         where y.Id == message.IdDialog
                         select x.Id).ToList();
            //всем пользователям диалога кроме отправителя ставлю статус сообщение в непрочитано
            foreach (var c in users)
            {
                if (c != message.IdSender)
                {
                    var messageStatus = new MessageStatus { Id = 0, IsChecked = false, UserId = c, Messages = messageTemp };
                    context.MessageStatus.Add(messageStatus);
                }
            }
            //добавляю к сообщению все фотографии
            foreach (var c in message.PhotosPath)
            {
                var photo = new Photos { Id = 0, Time = message.Time, Path = c, Message=messageTemp};
                context.Photos.Add(photo);
            }
            await context.SaveChangesAsync();
        }

        public async Task Update(MessageWithPhoto request)
        {
            var message = (from x in context.Dialogs.Include(x => x.Messages).ThenInclude(x=>x.Photos)
                           from y in x.Messages
                           where x.Id == request.IdDialog && y.Time == request.Time
                           select y).FirstOrDefault();
            message.Text = request.Message;
            var pathes = message.Photos.Select(x => x.Path).ToList();
            var deletePhotosPath = message.Photos.Select(x => x.Path).Except(request.PhotosPath).ToList();
            var deletePhotos = (from x in message.Photos
                                from y in deletePhotosPath
                                where x.Path == y
                                select x).ToList();
            foreach (var c in deletePhotos)
                message.Photos.Remove(c);
            foreach (var c in request.PhotosPath)
            {
                if (!pathes.Contains(c))
                {
                    var photo = new Photos { Id = 0, Path = c, Time = request.Time, MessageId = message.Id };
                    context.Photos.Add(photo);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
