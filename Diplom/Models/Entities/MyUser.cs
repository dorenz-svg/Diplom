using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Diplom.Models.Entities
{
    public class MyUser:IdentityUser
    {
        public IEnumerable<Friends> CurrentUser { get; set; }
        public IEnumerable<Friends> Friends { get; set; }
        public List<Dialogs> Dialogs { get; set; } = new List<Dialogs>();
        public IEnumerable<Messages> Messages { get; set; }
        public IEnumerable<MessageStatus> MessageStatus  { get; set; }

    }
}
