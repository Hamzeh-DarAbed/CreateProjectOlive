using CreateProjectOlive.Models;
using CreateProjectOlive.UnitOfWorks;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using CreateProjectOlive.Dtos;

namespace CreateProjectOlive.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        public AdminUserController(IUnitOfWork unitOfWork, IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _configuration = config;
            _userManager = userManager;
            _signInManager = signInManager;


        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody] AddAdminDto user)
        {
            if (ModelState.IsValid)
            {
                User appUser = new User
                {
                    UserName = user.Name,
                    Email = user.Email
                };
                try
                {
                    IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                    // await _unitOfWork.SaveAsync();
                    // return Ok("userCreated");
                    if (result.Succeeded)
                        return Ok("Admin User is created");
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AdminLogin(LoginAdminUserDto LoginData)
        {
            if (ModelState.IsValid)
            {
                User appUser = await _userManager.FindByEmailAsync(LoginData.Email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, LoginData.Password, false, false);
                    if (result.Succeeded)
                    {
                        var token = await GenerateToken(appUser);
                        return Ok(token);
                    }
                }
                return BadRequest("Wrong Email Or Password");
            }
            return BadRequest();
        }



        private async Task<string> GenerateToken(User user)
        {

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.NameId,user.Email), //modified
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Jwt:Issuer"),
                audience: _configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: DateTime.Now.AddHours(200),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}