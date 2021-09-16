using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Entities
{
    public class Photos
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public DateTime Time { get; set; }
        public long MessageId { get; set; }
        public Messages Message { get; set; }
        public long PostId { get; set; }
        public Posts Post { get; set; }
    }
}
