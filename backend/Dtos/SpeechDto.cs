using System;

namespace backend.Dtos
{
    public record SpeechDto
    {
        public byte[] data { get; init; }
        public string error { get; set; }
    } 
}