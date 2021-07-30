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
        private readonly IChatRepository repository;
        public ChatHub(IChatRepository repo) => repository = repo;
        public async Task Enter(long Id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Id.ToString());
        }
        public async Task Send(string message, long id, string userIdReceiver)
        {
            await repository.SetMessage(message, id, Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, userIdReceiver);
            await Clients.Group(id.ToString()).SendAsync("Receive", message);
        }
    }
}
