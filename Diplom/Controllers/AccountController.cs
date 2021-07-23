using Diplom.Infrastructure;
using Diplom.Models;
using Diplom.Models.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly DBContext _context;
        public AccountController(UserManager<User>userManager,SignInManager<User> signInManager, IJwtGenerator jwtGenerator, DBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
            _context = context;
        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> LoginAsync(LoginQuery query)
        {
            var user = await _userManager.FindByEmailAsync(query.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, query.Password, false);

            if (result.Succeeded)
            {
                return new AppUser
                {
                    UserName = user.UserName,
                    Token = _jwtGenerator.CreateToken(user)
                };
            }

            return Unauthorized();
        }
        [HttpPost("registration")]
        public async Task<ActionResult<AppUser>> RegistrationAsync([FromBody]RegistrationQuery query)
        {
            if (await _context.Users.Where(x => x.Email == query.Email).AnyAsync())
            {
                return BadRequest("Email already exist");
            }

            if (await _context.Users.Where(x => x.UserName == query.UserName).AnyAsync())
            {
                return BadRequest("UserName already exist");
            }

            var user = new User
            {
                Email = query.Email,
                UserName = query.UserName
            };

            var result = await _userManager.CreateAsync(user, query.Password);

            if (result.Succeeded)
            {
                return new AppUser
                {
                    Token = _jwtGenerator.CreateToken(user),
                    UserName = user.UserName,
                };
            }

            return BadRequest("Client creation failed");
        }
    }
}
