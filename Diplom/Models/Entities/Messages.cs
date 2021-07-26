using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Entities
{
    public class Messages
    {
        public long Id { get; set; }
        public long DialogsId { get; set; }
        public Dialogs Dialogs { get; set; }
        public string UserId { get; set; }
        public MyUser User { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
