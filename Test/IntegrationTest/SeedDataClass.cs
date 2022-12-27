using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using MongoOlive.DBContext;

namespace MongoOlive.Test.IntegrationTest
{

    public class SeedDataClass : ISeedDataClass
    {
        private readonly ApplicationDBContext _db;

        public SeedDataClass(ApplicationDBContext db)
        {
            _db = db;
        }

        public void InitializeDbForTests()
        {
            _db.Projects.AddRange(
                new Project
                {
                    Id = "2",
                    ProjectName = "Test Blog 1",
                    BusinessType = "Test Business 1",
                    CreatedBy = "Test CreatedBy 1",
                    Domain = "Test Domain 1",
                    ProjectDescription = "Test ProjectDescription 1"
                },
                new Project
                {
                   Id = "3",
                    ProjectName = "Test Blog 2",
                    BusinessType = "Test Business 2",
                    CreatedBy = "Test CreatedBy 2",
                    Domain = "Test Domain 2",
                    ProjectDescription = "Test ProjectDescription 2"
                },
                new Project
                {
                    Id = "4",
                    ProjectName = "Test Blog 3",
                    BusinessType = "Test Business 3",
                    CreatedBy = "Test CreatedBy 3",
                    Domain = "Test Domain 3",
                    ProjectDescription = "Test ProjectDescription 3"
                },
                new Project
                {
                    Id = "5",
                    ProjectName = "Test Blog 4",
                    BusinessType = "Test Business 4",
                    CreatedBy = "Test CreatedBy 4",
                    Domain = "Test Domain 4",
                    ProjectDescription = "Test ProjectDescription 4"
                },
                new Project
                {
                   Id = "6",
                    ProjectName = "Test Blog 5",
                    BusinessType = "Test Business 5",
                    CreatedBy = "Test CreatedBy 5",
                    Domain = "Test Domain 5",
                    ProjectDescription = "Test ProjectDescription 5"
                }
            );

            _db.SaveChanges(true);
        }
    }
}