using CreateProjectOlive.Models;
using MongoDB.Driver;

namespace CreateProjectOlive.Helper
{
    public class SeedToDBHelper
    {
        private static ApplicationUser _admin = new ApplicationUser();

        public SeedToDBHelper(SeedAdminConfig adminData)
        {
            Guid guid = Guid.NewGuid();
            _admin.Id = guid;

            _admin.UserName = adminData.UserName;
            _admin.NormalizedUserName = adminData.NormalizedUserName;
            _admin.Email = adminData.Email;
            _admin.NormalizedEmail = adminData.NormalizedEmail;
            _admin.PasswordHash = adminData.PasswordHashed;
            _admin.SecurityStamp = adminData.SecurityStamp;
        }
        public void SeedAdminUserIfNotExist(IMongoCollection<ApplicationUser> UserCollection)
        {
            bool exists = UserCollection.Find(_ => _.Email == _admin.Email).Any();

            if (!exists)
            {
                UserCollection.InsertOne(_admin);
            }
        }

        public static void SeedToDataToDb()
        {
            SeedAdminConfig adminData = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Admin").Get<SeedAdminConfig>();

            DataBaseConfig dataBaseConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MongoDb").Get<DataBaseConfig>();

            MongoClient client = new MongoClient(dataBaseConfig.ConnectionString);

            SeedToDBHelper helper = new SeedToDBHelper(adminData);

            var ApplicationUser = client.GetDatabase(dataBaseConfig.Database)
            .GetCollection<ApplicationUser>(typeof(ApplicationUser).Name);

            helper.SeedAdminUserIfNotExist(ApplicationUser);
        }
    }

    public class SeedAdminConfig
    {

        public string _id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string NormalizedUserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NormalizedEmail { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public string SecurityStamp { get; set; } = string.Empty;

    }


}
