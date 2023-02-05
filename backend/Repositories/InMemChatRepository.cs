using System;
using System.Collections.Generic;
using backend.Entities;
using System.Linq;

namespace backend.Repositories
{

    public class InMemChatRepository : IChatRepository
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

        public Chat GetChat(Guid id)
        {
            return chats.Where(chat => chat.Id == id).SingleOrDefault();
        }
        public void CreateChat(Chat chat)
        {
            chats.Add(chat);
        }
        public void UpdateChat(Chat chat)
        {
            var index = chats.FindIndex(existingChat => existingChat.Id == chat.Id);
            chats[index] = chat;
        }
    }
}