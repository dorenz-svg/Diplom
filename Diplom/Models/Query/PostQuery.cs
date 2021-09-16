using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class PostQuery
    {
        public string Id { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
    }
    public class PostWithPhotoQuery : PostQuery
    {
        public List<string> PhotosPath { get; set; }
    }
}
