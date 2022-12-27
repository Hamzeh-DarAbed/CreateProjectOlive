using Xunit;
using CreateProjectOlive.Dtos;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CreateProjectOlive.Test
{
    public class AdminUserControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {

        private readonly HttpClient _httpClient;
        public AdminUserControllerTest(TestingWebAppFactory<Program> factory)
            => _httpClient = factory.CreateClient();


        [Fact]
        public async Task TestAddAdminUser()
        {

            AddAdminDto AddAdminDto = new AddAdminDto
            {
                Name = "Admin2",
                Email = "admin2@admin.com",
                Password = "Admin#adfaf123"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(AddAdminDto), Encoding.UTF8, "application/json");


            using var response = await _httpClient.PostAsync("/api/AdminUser/Register", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal("Admin User is created", stringResult);
        }

        [Fact]
        public async Task TestAdminUserLogin()
        {


            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "admin2@admin.com",
                Password = "Admin#adfaf123"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/AdminUser/Login", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal((int)response.StatusCode, 200);

        }

        [Fact]
        public async Task TestAdminUserLoginWrongEmailOrPassword()
        {
            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "admin@optimumpartners.com",
                Password = "123456"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/AdminUser/Login", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal("Wrong Email Or Password", stringResult);
            Assert.Equal((int)response.StatusCode, 400);
        }





    }
}