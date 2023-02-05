using System;

namespace backend.Dtos
{
    public record ChatDto
    {
        public Guid Id { get; init; }
        public string Name { get;  init; }
    } 
}