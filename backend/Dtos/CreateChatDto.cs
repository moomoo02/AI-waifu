using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public record CreateChatDto
    {
        [Required]
        public string Name { get;  init; }
    }
}