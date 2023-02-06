using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record UpdateChatDto
    {
        [Required]
        public string Name { get; init; }
    }
}