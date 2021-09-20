using System;
using System.Collections.Generic;

namespace Diplom.Models.Entities
{
    public class Posts
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public MyUser User { get; set; }
        public List<Likes> Likes { get; set; }
        public List<Photos> Photos { get; set; }
    }
}
