using System.Linq.Expressions;
using MongoDB.Driver;

namespace Build.Common.MongoDB
{
    public class MongoRepository<T> : IRepository<T> where T:IEntity
    {
        private readonly IMongoCollection<T> dbCollection;
        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database,string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }
        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
             return await dbCollection.Find(filter).ToListAsync();
        }
        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(build => build.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(T build)
        {
            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }
            await dbCollection.InsertOneAsync(build);
        }
        public async Task UpdateAsync(T build)
        {
            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }
            FilterDefinition<T> filter = filterBuilder.Eq(exBuild => exBuild.Id, build.Id);
            await dbCollection.ReplaceOneAsync(filter, build);
        }
        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(build => build.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }        
    }
}