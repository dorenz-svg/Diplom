using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class MessageQuery
    {
        [BindRequired]
        public long IdDialog { get; set; }
        public string Message { get; set; }
        [BindRequired]
        public DateTime Time { get; set; }
        [BindNever]
        [JsonIgnore]
        public string IdSender { get; set; }
        [BindNever]
        [JsonIgnore]
        public List<string> PhotosPath { get; set; }
        public IFormFileCollection File { get; set; }
    }
}
