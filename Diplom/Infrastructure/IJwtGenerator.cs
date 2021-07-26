using Diplom.Models.Entities;

namespace Diplom.Infrastructure
{
    public interface IJwtGenerator
    {
        string CreateToken(MyUser user);
    }
}
