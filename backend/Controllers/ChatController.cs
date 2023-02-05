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
        //Get /items/id
        [HttpGet("{id}")]
        public ActionResult<ChatDto> GetChat(Guid id){
            var chat = repository.GetChat(id);

            if (chat is null)
            {
                return NotFound();
            }

            return Ok(chat.AsDto());
        }
    }
}