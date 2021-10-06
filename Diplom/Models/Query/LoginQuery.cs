using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Diplom.Models.Query
{
    public class LoginQuery
    {
        /// <summary>
        /// Email
        /// </summary>
        [BindRequired]
        public string Email { get; set; }
        [BindRequired]
        public string Password { get; set; }
    }
}
