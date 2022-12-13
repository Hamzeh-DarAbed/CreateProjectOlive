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
        private readonly IMongoCollection<TDocument> _projects;
        public Service(IOptions<ProjectDataBaseConfig> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionString);

            _projects = client.GetDatabase(options.Value.Database)
            .GetCollection<TDocument>(options.Value.Collection);
        }
        public async Task<IEnumerable<TDocument>> GetAll()
        {
            
         return await _projects.Find(p => true).ToListAsync();   
        }

        public async Task<TDocument> GetById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq("Id", id);
            return await _projects.FindAsync(filter).Result.FirstOrDefaultAsync();
        }
        

        public async Task Create(TDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            await _projects.InsertOneAsync(document);
           
        }

        public async Task<bool> Update(string id, TDocument document)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }
            FilterDefinition<TDocument> filter = Builders<TDocument>.Filter.Eq("Id", id);
            
            var result=await _projects.ReplaceOneAsync(filter, document);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var t=Builders<TDocument>.Filter.Eq("Id", id);
            var tf=await _projects.DeleteOneAsync(t);
            return tf.IsAcknowledged && tf.DeletedCount > 0;

        }
    }
}