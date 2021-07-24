using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Infrastructure
{
    public interface IJwtGenerator
    {
        string CreateToken(MyUser user);
    }
}
