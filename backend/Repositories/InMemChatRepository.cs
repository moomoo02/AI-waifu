using System;
using System.Collections.Generic;
using backend.Entities;
using System.Linq;

namespace backend.Repositories
{
    public class InMemChatRepository
    {
        private readonly List<Chat> chats = new()
        {
            new Chat { Id= Guid.NewGuid(), Name= "moomoo02" },
            new Chat { Id= Guid.NewGuid(), Name= "moomoo03" }
        };  

        public IEnumerable<Chat> GetChats()
        {
            return chats;
        }

        public Chat GetItem(Guid id)
        {
            return chats.Where(chat => chat.Id == id).SingleOrDefault();
        } 
    }
}