using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Response
{
    public class DialogResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Message Message { get; set; }
    }
    public class Message
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public string UserName { get; set; }
    }
}
