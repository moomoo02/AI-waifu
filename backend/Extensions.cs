using backend.Dtos;
using backend.Entities;

namespace backend
{
    public static class Extentions{
        public static ChatDto AsDto(this Chat chat)
        {
            return new ChatDto{
                Id = chat.Id,
                Name = chat.Name
            };
        }
    }
}