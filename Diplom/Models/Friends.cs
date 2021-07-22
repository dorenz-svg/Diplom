using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class Friends
    {
        public long Id { get; set; }
        public string User1Id { get; set; }
        public User User1 { get; set; }
        public string User2Id { get; set; }
        public User User2 { get; set; }

    }
}
