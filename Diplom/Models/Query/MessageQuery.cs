using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class MessageQuery
    {
        public long IdDialog { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string IdSender { get; set; }
    }
    public class MessageWithPhoto : MessageQuery
    {
        public List<string> PhotosPath { get; set; }
    }
}
