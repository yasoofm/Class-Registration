using ClassRegistrationBack.Models;
using ClassRegistrationBack.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassRegistrationBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly TokenService _tokenService;
        private readonly ClassContext _classContext;
        public AuthController(TokenService tokenService, ClassContext classContext)
        {
            _classContext = classContext ;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest user)
        {
            var response = _tokenService.GenerateToken(user.Username, user.Password);
            if (response.IsValid)
            {
                return Ok(new { Token = response.Token });
            }
            return BadRequest("Username and/or Password is incorrect");
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserRegisteration userRegistration)
        {
            bool isAdmin = false;
            if (_classContext.Users.Count() == 0)
            {
                isAdmin = true;
            }

            var newAccount = UserAccount.Create(userRegistration.UserName,userRegistration.Password,false);

            _classContext.Users.Add(newAccount);
            _classContext.SaveChanges();

            return Ok(new { Message = "User Created" });
        }
    }
}