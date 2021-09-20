using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class PostQuery
    {
        [BindRequired]
        public string Id { get; set; }
        [BindRequired]
        public DateTime Time { get; set; }
        public string Text { get; set; }
        [BindNever]
        public List<string> PhotosPath { get; set; }
        public IFormFileCollection File { get; set; }
    }
}
