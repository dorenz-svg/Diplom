using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Entities
{
    public class MessageStatus
    {
        public long Id { get; set; }
        public long MessagesId { get; set; }
        public Messages Messages { get; set; }
        public string UserId { get; set; }
        public MyUser User { get; set; }
        public bool IsChecked { get; set; }
    }
}
