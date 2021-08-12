using System;
namespace Diplom.Models.Entities
{
    public class Posts
    {
        public long Id { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public MyUser User { get; set; }
    }
}
