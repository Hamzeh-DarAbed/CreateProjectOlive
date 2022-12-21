using Xunit;
using CreateProjectOlive.Controllers;
using Microsoft.AspNetCore.Mvc;
using CreateProjectOlive.Dtos;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Identity;

namespace CreateProjectOlive.Test
{
    public class AdminUserControllerTest : OliveTestBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public IConfiguration _configuration;

        public AdminUserControllerTest(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _configuration = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Fact]
        public async void TestAdminUserLogin()
        {
            var controller = new AdminUserController(_unitOfWork, _configuration, _userManager, _signInManager);

            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "admin@optimumpartners.com",
                Password = "123456"
            };
            var response = await controller.AdminLogin(loginDto);
            Assert.IsType<OkObjectResult>(response);
        }

    }
}