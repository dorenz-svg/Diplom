using Diplom.Models.Entities;
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
    public class EFDialogRepository : IDialogRepository
    {
        private readonly DBContext context;
        public EFDialogRepository(DBContext ctx) => context = ctx;

        public async Task CreateDialog(string userId1, string userId2,string name)
        {
            var dialog = new Dialogs { Id = 0, Name = name, Time = DateTime.UtcNow, };
            var user1 = context.Users.Find(userId1);
            var user2 = context.Users.Find(userId2);
            context.Dialogs.Add(dialog);
            dialog.Users.Add(user1);
            dialog.Users.Add(user2);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDialog(long id)
        {
            var temp =context.Dialogs.Include(x=>x.Messages).ThenInclude(c=>c.MessageStatus).FirstOrDefault(x=>x.Id==id);
            context.Dialogs.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DialogResponse>> GetDialogs(string id)
        {
            var temp = (from x in context.Dialogs.Include(x => x.Users).Include(x=>x.Messages)
                        from user in x.Users
                        where user.Id == id
                        select new DialogResponse{Id=x.Id,Name=x.Name,
                            Message= x.Messages.Select(c=> 
                            new Message { Id = c.Id, Text = c.Text, Time = c.Time, UserName = user.UserName })
                            .FirstOrDefault()}).ToList();
            return await Task.FromResult(temp);
        }

        public async Task<IEnumerable<UserResponse>> GetUsersDialog(long id)
        {
            var temp = (from x in context.Users.Include(x => x.Dialogs)
                        from c in x.Dialogs
                        where c.Id == id
                        select new UserResponse {Id=x.Id,UserName=x.UserName,Email=x.Email,Phone=x.PhoneNumber }).ToList();
            return await Task.FromResult(temp);
        }        

        public async Task UpdateDialog(long id,string name)
        {
            var temp = context.Dialogs.Find(id);
            temp.Name = name;
            await context.SaveChangesAsync();
        }
    }
}
