using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Response
{
    public class PostsResponse
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public long Likes { get; set; }
        public bool IsLike { get; set; }
        public IEnumerable<string> Path { get; set; }
    }
}
