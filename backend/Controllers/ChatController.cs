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
        public IEnumerable<ChatDto> GetChats()
        {   
            return repository.GetChats().Select( chat => chat.AsDto());
        } 
        //Get /chat/id
        [HttpGet("{id}")]
        public ActionResult<ChatDto> GetChat(Guid id){
            var chat = repository.GetChat(id);

            if (chat is null)
            {
                return NotFound();
            }

            return Ok(chat.AsDto());
        }
        // POST /chat
        [HttpPost]
        public ActionResult<ChatDto> CreateChat(CreateChatDto chatDto)
        {
            Chat chat = new()
            {
                Id = Guid.NewGuid(),
                Name = chatDto.Name,
            };

            repository.CreateChat(chat);

            return CreatedAtAction(nameof(GetChat), new {Id = chat.Id}, chat.AsDto());
        }
        // Put  /chat
        [HttpPut("{id}")]
        public ActionResult<ChatDto> UpdateChat(Guid id, UpdateChatDto chatDto){
            var existingChat = repository.GetChat(id);

            if (existingChat is null)
            {
                return NotFound();
            }

            Chat updatedChat = new()
            {
                Id = id,
                Name = chatDto.Name
            };

            repository.UpdateChat(updatedChat);

            return NoContent();
        }
        // DELETE /chats
        [HttpDelete("{id}")]
        public ActionResult DeleteChat(Guid id)
        {
            var existingChat = repository.GetChat(id);
            
            if (existingChat is null)
            {
                return NotFound();
            }

            repository.DeleteChat(id);

            return NoContent();
        }
    }
}