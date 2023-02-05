using System;
using System.Collections.Generic;
using backend.Entities;

namespace backend.Repositories
{
    public interface IChatRepository
    {
        Chat GetChat(Guid id);
        IEnumerable<Chat> GetChats();
    }
}