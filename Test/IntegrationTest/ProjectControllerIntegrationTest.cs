using System.Net;
using System.Text;
using CreateProjectOlive.Models;
using Newtonsoft.Json;
using Xunit;

namespace CreateProjectOlive.Test.IntegrationTest
{
    public class ProjectControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Program>>
    {

        private readonly HttpClient _factory;

        public ProjectControllerIntegrationTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory.CreateClient();
        }

        [Theory]
        [InlineData("/GetProjects")]
        public async Task GetProjects_WhenProjectsExist_Returns200(string url)
        {
            HttpResponseMessage response = await _factory.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/CreateProject")]
        public async Task PostProjects_WhenProjectsExist_Returns201(string url)
        {

            HttpResponseMessage response = await _factory.PostAsync(url, new StringContent(JsonConvert.SerializeObject(new Project
            {

                Id = Guid.NewGuid(),
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
        [InlineData("/ProjectDetails/85d05ebb-6cfc-435a-a7c6-ae92a553431c")]
        public async Task GetProjectDetails_WhenProjectsExist_Returns200(string url)
        {

            HttpResponseMessage response = await _factory.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/DeleteProject/85d05ebb-6cfc-435a-a7c6-ae92a553431b")]
        public async Task DeleteProject_WhenProjectsExist_Returns200(string url)
        {

            HttpResponseMessage response = await _factory.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Theory]
        [InlineData("/UpdateProject/85d05ebb-6cfc-435a-a7c6-ae92a553431b")]
        public async Task UpdateProject_WhenProjectsExist_Returns200(string url)
        {

            HttpResponseMessage response = await _factory.PutAsync(url, new StringContent(JsonConvert.SerializeObject(new Project
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

        [Theory]
        [InlineData("/ProjectDetails/85d05ebb-6cfc-435a-a7c6-ae92a563431b")]
        public async Task GetProjectDetails_WhenProjectsNotExist_Returns404(string url)
        {

            HttpResponseMessage response = await _factory.GetAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/DeleteProject/85d05ebb-6cfc-435a-a7c6-ae92a563431b")]
        public async Task DeleteProject_WhenProjectsNotExist_Returns404(string url)
        {

            HttpResponseMessage response = await _factory.DeleteAsync(url);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/UpdateProject/85d05ebb-6cfc-435a-a7c6-ae92a563431b")]
        public async Task UpdateProject_WhenProjectsNotExist_Returns404(string url)
        {

            HttpResponseMessage response = await _factory.PutAsync(url, new StringContent(JsonConvert.SerializeObject(new Project
            {

                ProjectName = "Updated Test Blog 3",
                BusinessType = "Updated Test Business 3",
                CreatedBy = "Updated Test CreatedBy 3",
                Domain = "Updated Test Domain 3",
                ProjectDescription = "Updated Test ProjectDescription 3"
            }), Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }



    }
}