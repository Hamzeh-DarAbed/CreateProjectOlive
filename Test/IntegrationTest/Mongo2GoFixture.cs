using CreateProjectOlive.Models;
using MongoDB.Driver;
using Mongo2Go;

namespace CreateProjectOlive.Test.IntegrationTest
{

    public class Mongo2GoFixture : IDisposable
{
        public MongoClient Client { get; }
 
        public IMongoDatabase Database { get; }
 
        public string ConnectionString { get; }
 
          private readonly MongoDbRunner _mongoRunner;
 
        private readonly string _databaseName = "my-database";
 
        public IMongoCollection<Project> DataBoundCollection { get; }
 
        public Mongo2GoFixture()
        {
            // initializes the instance
            _mongoRunner = MongoDbRunner.Start();
 
            // store the connection string with the chosen port number
            ConnectionString = _mongoRunner.ConnectionString;
            // create a client and database for use outside the class
            Client = new MongoClient(ConnectionString);
 
            Database = Client.GetDatabase(_databaseName);
 
            // initialize your collection
            DataBoundCollection = Database.GetCollection<Project>(typeof(Project).Name);
        }
 
        public void SeedData()
        {
            // seed data

            var project = new Project
            {
                Id = "5f1b7b9b9c9d440000a1b1b5",
                ProjectDescription = "Test Project",
                ProjectName = "Test Project",
                BusinessType = "Test Project",
                CreatedBy = "Test Project",
                Domain = "Test Project"
            };

            DataBoundCollection.InsertOne(project);
        } 
        // GetFilePath using DockerFixture's approach
        private string GetFilePath(string file)
        {
            var path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), file);
            return path;
        }
        
 
        public void Dispose()
        {
            //DropDatabase
            Database.DropCollection(typeof(Project).Name);
            // dispose the runner
            _mongoRunner.Dispose();
        }
}
}