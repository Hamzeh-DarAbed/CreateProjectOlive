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
        private RoleManager<IdentityRole> _roleManager;

        public IConfiguration _configuration;
        public AdminUserController(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _configuration = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("RegisterSuperAdmin")]
        public async Task<IActionResult> Post([FromBody] AddAdminDto user)
        {
            if (ModelState.IsValid)
            {
                User appUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                try
                {
                    IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                    if (result.Succeeded)
                    {
                        var AdminRole = await _roleManager.FindByNameAsync("SuperAdmin");
                        if (AdminRole != null)
                        {
                            IdentityResult roleResult = await _userManager.AddToRoleAsync(appUser, AdminRole.Name);
                            if (roleResult.Succeeded)
                            {
                                return Ok("Admin User is created");
                            }
                            else
                            {
                                return BadRequest(roleResult.Errors);
                            }
                        }
                    }
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
        [Route("LoginSuperAdmin")]
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
                        var role = await _userManager.GetRolesAsync(appUser);
                        if (role.Contains("SuperAdmin"))
                        {
                            var token = GenerateToken(appUser);
                            return Ok(token);
                        }
                    }
                }
                return BadRequest("Wrong Email Or Password");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateBusinessOwner")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto user)
        {
            if (ModelState.IsValid)
            {
                User appUser = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                try
                {
                    IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                    if (result.Succeeded)
                    {
                        var AdminRole = _roleManager.FindByNameAsync("BusinessOwner").Result;
                        if (AdminRole != null)
                        {
                            IdentityResult roleResult = await _userManager.AddToRoleAsync(appUser, AdminRole.Name);
                            if (roleResult.Succeeded)
                            {

                                return Ok("User is created");
                            }
                            else
                            {
                                return BadRequest(roleResult.Errors);
                            }
                        }
                    }
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

        private string GenerateToken(User user)
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