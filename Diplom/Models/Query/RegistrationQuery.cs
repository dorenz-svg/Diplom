using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Diplom.Models.Query
{
    public class RegistrationQuery
    {
        [BindRequired]
        public string UserName { get; set; }
        [BindRequired]
        public string Email { get; set; }
        [BindRequired]
        public string Password { get; set; }
    }
}
