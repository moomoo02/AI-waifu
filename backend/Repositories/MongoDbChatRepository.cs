using backend.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class MongoDbChatRepository : IChatRepository
    {
        private const string databaseName = "backend";
        private const string collectionName = "chats";
        private readonly IMongoCollection<Chat> chatCollection;
        private readonly FilterDefinitionBuilder<Chat> filterBuilder = Builders<Chat>.Filter;
        public MongoDbChatRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            chatCollection = database.GetCollection<Chat>(collectionName);
        }
        public async Task CreateChatAsync(Chat chat)
        {
            await chatCollection.InsertOneAsync(chat);
        }

        public async Task DeleteChatAsync(Guid id)
        {
            var filter = filterBuilder.Eq(chat => chat.Id, id);
            await chatCollection.DeleteOneAsync(filter);
        }

        public async Task<Chat> GetChatAsync(Guid id)
        {
            var filter = filterBuilder.Eq(chat => chat.Id, id);
            return await chatCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Chat>> GetChatsAsync()
        {
            return await chatCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateChatAsync(Chat chat)
        {
            var filter = filterBuilder.Eq(existingChat => existingChat.Id, chat.Id);
            await chatCollection.ReplaceOneAsync(filter, chat);
        }
    }
}