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
        public Task CreateChatAsync(Chat chat)
        {
            chatCollection.InsertOneAsync(chat);
        }

        public void DeleteChatAsync(Guid id)
        {
            var filter = filterBuilder.Eq(chat => chat.Id, id);
            chatCollection.DeleteOne(filter);
        }

        public Chat GetChatAsync(Guid id)
        {
            var filter = filterBuilder.Eq(chat => chat.Id, id);
            return chatCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Chat> GetChatsAsync()
        {
            return chatCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateChatAsync(Chat chat)
        {
            var filter = filterBuilder.Eq(existingChat => existingChat.Id, chat.Id);
            chatCollection.ReplaceOne(filter, chat);
        }
    }
}