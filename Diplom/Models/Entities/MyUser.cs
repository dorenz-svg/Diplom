using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Diplom.Models.Entities
{
    public class MyUser:IdentityUser
    {
        public IEnumerable<Friends> CurrentUser { get; set; }
        public IEnumerable<Friends> Friends { get; set; }
        public IEnumerable<Dialogs> Dialogs { get; set; }
        public IEnumerable<Messages> Messages { get; set; }

    }
}
