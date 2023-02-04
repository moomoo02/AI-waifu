using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using backend.Entities;
using System.Collections.Generic;

namespace backend.Controllers
{
    [ApiController]
    [Route("chat")]

    public class ChatController : ControllerBase
    {
        private readonly InMemChatRepository repository;

        public ChatController(){
            repository = new InMemChatRepository();
        }
        // GET /items
        [HttpGet]
        public IEnumerable<Chat> GetChats()
        {
            return repository.GetChats();
        }
    }
}