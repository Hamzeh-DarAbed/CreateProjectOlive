using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace CreateProjectOlive.Test.IntegrationTest
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {

        // Injects the Mongo2go configuration settings into the running application
        public WebApplicationFactory<Program> InjectMongoDbConfigurationSettings(string connectionString, string database)
        {


            return WithWebHostBuilder(builder =>
            {
                builder.UseContentRoot(".");
                builder.ConfigureTestServices(services =>
                {
                    services.Configure<DataBaseConfig>(opts =>
                    {
                        opts.ConnectionString = connectionString;
                        opts.Database = database;
                    });

                });
            });
        }
    }
}