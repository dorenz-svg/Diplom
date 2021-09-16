using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string UserId { get; set; }
        public MyUser User { get; set; }
        [Required]
        [MaxLength(300)]
        public string Text { get; set; }
        public List<MessageStatus> MessageStatus { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public List<Photos> Photos { get; set; }
    }
}
