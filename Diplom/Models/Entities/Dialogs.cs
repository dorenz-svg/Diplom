using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Entities
{
    public class Dialogs
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        public IEnumerable<MyUser> Users { get; set; }
        public IEnumerable<Messages> Messages { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
