using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models.Query
{
    public class DialogsQuery
    {
        [BindRequired]
        public string UserId { get; set; }
        [BindRequired]
        public string Name { get; set; }
    }
}
