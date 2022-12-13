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
        public Service(IOptions<DataBaseConfig> options)
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
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }


        public async Task Create(TDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            await _collection.InsertOneAsync(document);

        }

        public async Task<bool> Update(string id, TDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("Id", id);

            var result = await _collection.ReplaceOneAsync(filter, document);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var t = Builders<TDocument>.Filter.Eq("Id", id);
            var tf = await _collection.DeleteOneAsync(t);
            return tf.IsAcknowledged && tf.DeletedCount > 0;

        }
    }
}