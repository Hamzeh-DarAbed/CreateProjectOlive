using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace CreateProjectOlive.Models
{
    [CollectionName("ApplicationUser")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {


    }
}