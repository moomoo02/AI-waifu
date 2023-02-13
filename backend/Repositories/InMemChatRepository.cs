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

        public IEnumerable<Chat> GetChatsAsync()
        {
            return chats;
        }

        public Chat GetChatAsync(Guid id)
        {
            return chats.Where(chat => chat.Id == id).SingleOrDefault();
        }
        public void CreateChatAsync(Chat chat)
        {
            chats.Add(chat);
        }
        public void UpdateChatAsync(Chat chat)
        {
            var index = chats.FindIndex(existingChat => existingChat.Id == chat.Id);
            chats[index] = chat;
        }
        public void DeleteChatAsync(Guid id)
        {
            var index = chats.FindIndex(existingChat => existingChat.Id == id);
            chats.RemoveAt(index);  
        }
    }
}