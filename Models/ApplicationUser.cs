using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore;
using AspNetCore.Identity.MongoDbCore.Models;

namespace CreateProjectOlive.Models
{

    public class ApplicationUser : MongoIdentityUser<Guid>
    {


    }
}