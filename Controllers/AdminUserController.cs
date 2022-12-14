using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using CreateProjectOlive.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreateProjectOlive.Controllers
{
    [Route("api/[controller]")]
    public class AdminUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<ApplicationRole> _roleManager;

        public AdminUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAdminDto user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return Ok("Admin User is created");
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = roleName });
                if (result.Succeeded)
                {
                    return Ok("Role was Added");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> AdminLogIn([FromBody] LoginAdminUserDto LoginData)
        {
            if (ModelState.IsValid)
            {
                //public string Email { get; set; } = null!;
                //public string Password { get; set; } = null!;
                ApplicationUser appUser = await _userManager.FindByEmailAsync(LoginData.Email);
                if (appUser != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, LoginData.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok("you are in");
                    }
                    else
                    {
                        return BadRequest("Wrong Pass");
                    }
                }
                else
                {
                    return BadRequest("Wrong Email");
                }

            }
            return BadRequest();
        }




    }
}