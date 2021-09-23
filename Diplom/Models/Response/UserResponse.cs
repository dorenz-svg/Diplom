using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Response
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProfilePhoto { get; set; }
        public IEnumerable<UserPosts> UserPosts { get; set; }
    }
    public class UserPosts {
        public long Id { get; set; }
        public IEnumerable<string> PhotosUrl { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
    }
}
