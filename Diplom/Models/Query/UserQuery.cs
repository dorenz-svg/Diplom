using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class UserQuery
    {
        [BindNever]
        [JsonIgnore]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
    }
}
