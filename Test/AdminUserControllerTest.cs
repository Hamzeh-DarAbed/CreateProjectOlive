using Xunit;
using CreateProjectOlive.Dtos;
using Newtonsoft.Json;
using System.Text;
using CreateProjectOlive.Models;

namespace CreateProjectOlive.Test
{
    public class AdminUserControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _httpClient;
        public AdminUserControllerTest(CustomWebApplicationFactory<Program> factory)
            => _httpClient = factory.CreateClient();


        [Fact]
        public async Task TestAdminUserLogin()
        {


            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "adminTest@adminTest.com",
                Password = "123456"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/AdminUser/LoginSuperAdmin", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal(200, (int)response.StatusCode);

        }

        [Fact]
        public async Task TestAdminUserLoginWrongEmailOrPassword()
        {
            LoginAdminUserDto loginDto = new LoginAdminUserDto
            {
                Email = "admin@admin.com",
                Password = "15475"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(loginDto), Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync("/api/AdminUser/LoginSuperAdmin", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();

            Assert.Equal("Wrong Email Or Password", stringResult);
            Assert.Equal(400, (int)response.StatusCode);
        }

        [Fact]
        public async Task TestSuperAdminIsSeedsIntoDatabaseOnCreate()
        {
            AddAdminDto AddAdminDto = new AddAdminDto
            {
                UserName = "Admin2",
                Email = "admin@optimumpartners.com",
                Password = "Admin123@Admin"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(AddAdminDto), Encoding.UTF8, "application/json");


            using var response = await _httpClient.PostAsync("/api/AdminUser/RegisterSuperAdmin", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("[{\"code\":\"DuplicateEmail\",\"description\":\"Email 'admin@optimumpartners.com' is already taken.\"}]", stringResult);

        }

        [Fact]
        public async Task TestCreateSuperAdmin()
        {
            AddAdminDto AddAdminDto = new AddAdminDto
            {
                UserName = "Admin22",
                Email = "admin@admin2.com",
                Password = "Admin123@Admin"
            };

            var HttpContent = new StringContent(JsonConvert.SerializeObject(AddAdminDto), Encoding.UTF8, "application/json");


            using var response = await _httpClient.PostAsync("/api/AdminUser/RegisterSuperAdmin", HttpContent);
            var stringResult = await response.Content.ReadAsStringAsync();
            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal("Admin User is created", stringResult);

        }




    }
}