using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class User:IdentityUser
    {
        public new long Id { get; set; }
        public IEnumerable<Friends> CurrentUser { get; set; }
        public IEnumerable<Friends> Friends { get; set; }
        
    }
}
