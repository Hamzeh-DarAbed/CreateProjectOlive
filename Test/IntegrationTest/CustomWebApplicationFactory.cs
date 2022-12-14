using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace CreateProjectOlive.Test.IntegrationTest
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        
        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }        
 
        // Injects the Mongo2go configuration settings into the running application
        public WebApplicationFactory<Startup> InjectMongoDbConfigurationSettings(string connectionString, string database)
        {
            return WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Configure<ProjectDataBaseConfig>(opts =>
                    {
                        opts.ConnectionString = connectionString;
                        opts.Database = database;
                    });
 
                });
            });
        }
    }
}