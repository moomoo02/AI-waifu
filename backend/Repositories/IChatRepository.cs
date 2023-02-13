using System;
using System.Collections.Generic;
using backend.Entities;

namespace backend.Repositories
{
    public interface IChatRepository
    {
        Task<Chat> GetChatAsync(Guid id);
        Task<IEnumerable<Chat>> GetChatsAsync();
        Task CreateChatAsync(Chat chat);
        Task UpdateChatAsync(Chat chat);
        Task DeleteChatAsync(Guid id);
    }
}