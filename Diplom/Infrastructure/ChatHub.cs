using Diplom.Models.Query;
using Diplom.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Infrastructure
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageRepository mesRepo;
        private readonly IDialogRepository diaRepo;
        public ChatHub(IMessageRepository mrepo,IDialogRepository drepo) {
            mesRepo = mrepo;
            diaRepo = drepo;
        }
        /// <summary>
        /// подключение пользователя ко всем диалогам
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns></returns>
        public async Task Enter(string id)
        {
            var dialogs = await diaRepo.GetDialogs(id);
            foreach(var c in dialogs)
                await Groups.AddToGroupAsync(Context.ConnectionId, c.IdDialog.ToString());
        }
        /// <summary>
        /// отправка сообщения всем пользователям в диалоге
        /// </summary>
        /// <param name="message">отправляемое сообщение</param>
        /// <param name="id">id диалога</param>
        /// <param name="userIdReceiver"></param>
        /// <returns></returns>
        public async Task Send(string message, long id)
        {
            await mesRepo.SetMessage(message, id, Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await Clients.Group(id.ToString()).SendAsync("Receive",new { message,id});
        }
        /// <summary>
        /// изменить сообщение и отправить всем пользователям в диалоге изменения
        /// </summary>
        /// <param name="id">id диалога</param>
        /// <param name="time">время сообщения</param>
        /// <param name="message">новое сообщение</param>
        /// <returns></returns>
        public async Task Update(long id,DateTime time, string message)
        {
            await mesRepo.Update(new MessageQuery { Id=id,Time=time,Message=message});
            await Clients.Group(id.ToString()).SendAsync("Update",new { message,id });
        }
        /// <summary>
        /// удалить сообщение и отправить всем пользователям диалога изменения
        /// </summary>
        /// <param name="id">id диалога</param>
        /// <param name="time">время создания сообщения</param>
        /// <returns></returns>
        public async Task Delete(long id,DateTime time)
        {
            await mesRepo.Delete(id,time);
            await Clients.Group(id.ToString()).SendAsync("Delete",new {id, time});
        }
    }
}
