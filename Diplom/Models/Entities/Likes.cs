using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Entities
{
    public class Likes
    {
        public MyUser User { get; set; }
        public string UserId { get; set; }
        public long PostId { get; set; }
        public Posts Posts { get; set; }
    }
}
