using Diplom.Infrastructure;
using Diplom.Models;
using Diplom.Models.Entities;
using Diplom.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;

        private readonly SignInManager<MyUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly DBContext _context;
        public AccountController(UserManager<MyUser>userManager,SignInManager<MyUser> signInManager, IJwtGenerator jwtGenerator, DBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _context = context;
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<object>> LoginAsync(LoginQuery query)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Пользователь не найден" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

            if (result.Succeeded)
            {
                return new
                {
                    id=user.Id,
                    UserName = user.UserName,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }

            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<ActionResult<object>> RegistrationAsync([FromBody]RegistrationQuery query)
        {
            if (await _context.Users.Where(x => x.Email == query.Email).AnyAsync())
            {
                return BadRequest(new {message= "Email already exist" });
            }

            if (await _context.Users.Where(x => x.UserName == query.UserName).AnyAsync())
            {
                return BadRequest(new { message = "UserName already exist" });
            }

            var user = new MyUser
            {
                Email = query.Email,
                UserName = query.UserName
            };

            var result = await _userManager.CreateAsync(user, query.Password);

            if (result.Succeeded)
            {
                return new
                {
                    id = user.Id,
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };
            }

            return BadRequest(new { message = "Client creation failed" });
        }
    }
}
