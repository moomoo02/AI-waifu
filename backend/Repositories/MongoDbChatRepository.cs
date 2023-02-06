using backend.Entities;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class MongoDbChatRepository : IChatRepository
    {
        private const string databaseName = "backend";
        private const string collectionName = "chats";
        private readonly IMongoCollection<Chat> chatCollection;
        public MongoDbChatRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            chatCollection = database.GetCollection<Chat>(collectionName);
        }
        public void CreateChat(Chat chat)
        {
            chatCollection.InsertOne(chat);
        }

        public void DeleteChat(Guid id)
        {
            throw new NotImplementedException();
        }

        public Chat GetChat(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Chat> GetChats()
        {
            throw new NotImplementedException();
        }

        public void UpdateChat(Chat chat)
        {
            throw new NotImplementedException();
        }
    }
}