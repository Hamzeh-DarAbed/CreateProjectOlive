using System.Text;
using AutoMapper;
using CreateProjectOlive.Dtos;
using CreateProjectOlive.Models;
using Newtonsoft.Json;
using Xunit;

namespace CreateProjectOlive.Test.IntegrationTest
{
    public class Mongo2GoIntegrationTest : IClassFixture<CustomWebApplicationFactory>, IClassFixture<Mongo2GoFixture>
    {
        private readonly CustomWebApplicationFactory _factory;
        private Mongo2GoFixture _mongoDb;

        

        public Mongo2GoIntegrationTest(CustomWebApplicationFactory factory, Mongo2GoFixture mongoDb)
        {
            _mongoDb = mongoDb;
            _factory = factory;
        }


        [Theory]
        [InlineData("GetProjects")]
        public async Task GetProjects_WhenProjectsExist_Returns200(string url)
        {
            _mongoDb.SeedData();

            // Arrange            
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(200, ((int)response.StatusCode));
        }

        [Theory]
        [InlineData("ProjectDetails/5f1b7b9b9c9d440000a1b1b5")]
        public async Task ProjectDetails_WhenProjectExists_Returns200(string url)
        {
            _mongoDb.SeedData();

            // Arrange            
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(200, ((int)response.StatusCode));
        }


        [Theory]
        [InlineData("CreateProject")]
        public async Task CreateProject_WithValidData_Returns201(string url)
        {

            CreateProjectDto projectDto = new CreateProjectDto
            {
                ProjectName = "Test Project",
                ProjectDescription = "Test Description",
                BusinessType = "Test Business Type",
                CreatedBy = "Test Created By",
                Domain = "Test Domain"
            };

            HttpContent HttpContent = new StringContent(JsonConvert.SerializeObject(projectDto), Encoding.UTF8, "application/json");
            // Arrange
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.PostAsync(url, HttpContent);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(201, ((int)response.StatusCode));
        }

        [Theory]
        [InlineData("UpdateProject/5f1b7b9b9c9d440000a1b1b5")]
        public async Task UpdateProject_WithValidData_Returns200(string url)
        {
            _mongoDb.SeedData();
            Project projectDto = new Project
            {
                ProjectName = "Test Updated Project",
                ProjectDescription = "Updated Description",
                BusinessType = "Test Business Type",
                CreatedBy = "Test Created By",
                Domain = "Test Domain"
            };

            HttpContent HttpContent = new StringContent(JsonConvert.SerializeObject(projectDto), Encoding.UTF8, "application/json");
            // Arrange
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.PutAsync(url, HttpContent);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(200, ((int)response.StatusCode));
        }

        [Theory]
        [InlineData("DeleteProject/5f1b7b9b9c9d440000a1b1b5")]
        public async Task DeleteProject_WhenProjectExists_Returns204(string url)
        {
            _mongoDb.SeedData();
            // Arrange
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.DeleteAsync(url);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(204, ((int)response.StatusCode));
        }

        [Theory]
        [InlineData("ProjectDetails/5f1b7b99c9d440000a1b15")]
        public async Task ProjectDetails_WhenProjectDoseNotExists_Returns400(string url)
        {
            _mongoDb.SeedData();

            // Arrange            
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.GetAsync(url);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(400, ((int)response.StatusCode));
        }

        [Theory]
        [InlineData("UpdateProject/5f1b7b9c9d44000a1b15")]
        public async Task UpdateProject_WhenProjectDoseNotExists_Returns400(string url)
        {
            _mongoDb.SeedData();
            Project projectDto = new Project
            {
                ProjectName = "Test Updated Project",
                ProjectDescription = "Updated Description",
                BusinessType = "Test Business Type",
                CreatedBy = "Test Created By",
                Domain = "Test Domain"
            };

            HttpContent HttpContent = new StringContent(JsonConvert.SerializeObject(projectDto), Encoding.UTF8, "application/json");
            // Arrange
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.PutAsync(url, HttpContent);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(400, ((int)response.StatusCode));
        }

        [Theory]
        [InlineData("DeleteProject/5f1b7b99c94000a1b15")]
        public async Task DeleteProject_WhenProjectDoseNotExists_Returns400(string url)
        {
            _mongoDb.SeedData();
            // Arrange
            HttpClient client = _factory.InjectMongoDbConfigurationSettings(_mongoDb.ConnectionString, _mongoDb.Database.DatabaseNamespace.DatabaseName).CreateClient();

            // Act
            HttpResponseMessage response = await client.DeleteAsync(url);

            _mongoDb.Database.DropCollection("Project");

            // Assert
            Assert.Equal(400, ((int)response.StatusCode));
        }



    }
}