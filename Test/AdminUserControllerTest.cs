using Xunit;
using CreateProjectOlive.Dtos;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CreateProjectOlive.Test
{
    public class AdminUserControllerTest : TestBase
    {

        public AdminUserControllerTest(WebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async void TestAddAdminUser()
        {


            AddAdminDto AddAdminDto = new AddAdminDto
            {
                Name = "Admin",
                Email = "admin@admin.com",
                Password = "Admin#adfaf123"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(AddAdminDto), Encoding.UTF8, "application/json");


            using var response = await _httpClient.PostAsync("/api/AdminUser/Register", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal((int)response.StatusCode, 200);
            Assert.Equal("Admin User is created", stringResult);
        }

        [Fact]
        public async void TestAdminUserLogin()
        {


            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "admin@admin.com",
                Password = "Admin#adfaf123"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/AdminUser/Login", HttpContent);

            Assert.Equal((int)response.StatusCode, 200);

        }

        [Fact]
        public async void TestAdminUserLoginWrongEmailOrPassword()
        {
            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "admin@admin.com",
                Password = "123123132"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/AdminUser/Login", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal("Wrong Email Or Password", stringResult);
            Assert.Equal((int)response.StatusCode, 400);
        }





    }
}