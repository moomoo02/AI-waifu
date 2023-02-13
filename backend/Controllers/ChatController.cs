using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using backend.Entities;
using System.Collections.Generic;
using System;
using backend.Dtos;
namespace backend.Controllers
{
    [ApiController]
    [Route("chat")]

    public class ChatController : ControllerBase
    {
        private readonly IChatRepository repository;

        public ChatController(IChatRepository repository){
            this.repository = repository;
        }
        // GET /items
        [HttpGet]
        public async Task<IEnumerable<ChatDto>> GetChatsAsync()
        {   
            return (await repository.GetChatsAsync())
                    .Select( chat => chat.AsDto());
        } 
        //Get /chat/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ChatDto>> GetChatAsync(Guid id){
            var chat = await repository.GetChatAsync(id);

            if (chat is null)
            {
                return NotFound();
            }

            return Ok(chat.AsDto());
        }
        // POST /chat
        [HttpPost]
        public async Task<ActionResult<ChatDto>> CreateChat(CreateChatDto chatDto)
        {
            Chat chat = new()
            {
                Id = Guid.NewGuid(),
                Name = chatDto.Name,
            };

            await repository.CreateChatAsync(chat);

            return CreatedAtAction(nameof(GetChatAsync), new {Id = chat.Id}, chat.AsDto());
        }
        // Put  /chat
        [HttpPut("{id}")]
        public async Task<ActionResult<ChatDto>> UpdateChat(Guid id, UpdateChatDto chatDto){
            var existingChat = repository.GetChatAsync(id);

            if (existingChat is null)
            {
                return NotFound();
            }

            Chat updatedChat = new()
            {
                Id = id,
                Name = chatDto.Name
            };

            await repository.UpdateChatAsync(updatedChat);

            return NoContent();
        }
        // DELETE /chats
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChat(Guid id)
        {
            var existingChat = repository.GetChatAsync(id);
            
            if (existingChat is null)
            {
                return NotFound();
            }

            await repository.DeleteChatAsync(id);

            return NoContent();
        }
    }
}