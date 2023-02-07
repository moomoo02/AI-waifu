using System;

namespace backend.Dtos
{
    public record MessageDto
    {
        public string direction { get; init; }
        public string content { get; init; }
    } 
}