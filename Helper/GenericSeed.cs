// using System.Reflection;
// using CreateProjectOlive.Models;
// using MongoDB.Driver;

// namespace CreateProjectOlive.Helper
// {
//     public class GenericSeed<T> where T : class
//     {

//         public virtual void SeedToDataToDb()
//         {
//             DataBaseConfig dataBaseConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("MongoDb").Get<DataBaseConfig>();


//         }

//         public void SeedDataIfNotExist(IMongoCollection<T> MyCollection, string Checker, string CheckerValue)
//         {

//             Type t = MyCollection.GetType();

//             PropertyInfo prop = t.GetProperty(Checker);

//             object val = prop.GetValue(CheckerValue);


//             bool exists = MyCollection.Find(_ => _.prop == val).Any();

//             if (!exists)
//             {
//                 UserCollection.InsertOne(_admin);
//             }
//         }
//     }
// }