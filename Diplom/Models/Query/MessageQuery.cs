using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class MessageQuery
    {
        public string Message { get; set; }
        public long IdDialog { get; set; }
        public string IdReceiver { get; set; }
    }
}
