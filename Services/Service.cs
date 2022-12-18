using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreateProjectOlive.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CreateProjectOlive.Services
{
    public class Service<TDocument> : IService<TDocument> where TDocument : class
    {
        private readonly IMongoCollection<TDocument> _collection;
        public Service(IOptions<ProjectDataBaseConfig> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _collection = client.GetDatabase(options.Value.Database)
            .GetCollection<TDocument>(typeof(TDocument).Name);
        }
        public async Task<IEnumerable<TDocument>> GetAll()
        {

            return await _collection.Find(p => true).ToListAsync();
        }

        public async Task<TDocument> GetById(string id)
        {
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }


        public async Task Create(TDocument document)
        {

            await _collection.InsertOneAsync(document);
        }

        public async Task<bool> Update(string id, TDocument document)
        {
            

            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("Id", id);

            ReplaceOneResult result = await _collection.ReplaceOneAsync(filter, document);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("Id", id);
            DeleteResult result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged && result.DeletedCount > 0;

        }
    }
}