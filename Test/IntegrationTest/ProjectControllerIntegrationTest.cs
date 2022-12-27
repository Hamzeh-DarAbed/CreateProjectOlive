using System.Net;
using System.Text;
using CreateProjectOlive.Models;
using Microsoft.EntityFrameworkCore;
using MongoOlive.DBContext;
using MongoOlive.Test.IntegrationTest;
using Newtonsoft.Json;
using Xunit;

namespace CreateProjectOlive.Test.IntegrationTest
{
    [Collection("InMemoryDatabase")]
    public class ProjectControllerIntegrationTest : IClassFixture<GenericWebApplicationFactory<Program, ApplicationDBContext, SeedDataClass>>
    {

        private readonly GenericWebApplicationFactory<Program, ApplicationDBContext, SeedDataClass> _factory;

        public ProjectControllerIntegrationTest(GenericWebApplicationFactory<Program, ApplicationDBContext, SeedDataClass> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/GetProjects")]
        public async Task GetProjects_WhenProjectsExist_Returns200(string url)
        {


            HttpClient client = _factory.CreateClient();


            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/CreateProject")]
        public async Task PostProjects_WhenProjectsExist_Returns201(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(new Project
            {
                Id = "1",
                ProjectName = "Test Blog 1",
                BusinessType = "Test Business 1",
                CreatedBy = "Test CreatedBy 1",
                Domain = "Test Domain 1",
                ProjectDescription = "Test ProjectDescription 1"
            }), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("/ProjectDetails/4")]
        public async Task GetProjectDetails_WhenProjectsExist_Returns200(string url)
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/DeleteProject/2")]
        public async Task DeleteProject_WhenProjectsExist_Returns200(string url)
        {
            HttpClient client = _factory.CreateClient();

            HttpResponseMessage response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("/UpdateProject/3")]
        public async Task UpdateProject_WhenProjectsExist_Returns200(string url)
        {
            HttpClient client = _factory.CreateClient();

            HttpResponseMessage response = await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(new Project
            {
                
                ProjectName = "Updated Test Blog 3",
                BusinessType = "Updated Test Business 3",
                CreatedBy = "Updated Test CreatedBy 3",
                Domain = "Updated Test Domain 3",
                ProjectDescription = "Updated Test ProjectDescription 3"
            }), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


    }
}