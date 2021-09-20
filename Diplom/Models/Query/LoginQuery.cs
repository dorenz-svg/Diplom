﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Diplom.Models.Query
{
    public class LoginQuery
    {
        [BindRequired]
        public string Email { get; set; }
        [BindRequired]
        public string Password { get; set; }
    }
}
