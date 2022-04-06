using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Diplom.Models.Query
{
    public class DialogsQuery
    {
        [BindRequired]
        public string UserName { get; set; }
        [BindRequired]
        public string DialogName { get; set; }
    }
}
