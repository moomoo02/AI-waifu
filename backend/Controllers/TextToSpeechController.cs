using System.Diagnostics;
using backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace Backend.Controllers
{
    [ApiController]
    [Route("text-speech")]
    public class TextToSpeechController : ControllerBase
    {
        public TextToSpeechController(){
        }
        //GET /text-speech/prompt
        [HttpGet("{prompt}")]
        public ActionResult<string> GetSpeech()
        {

            return "NotImplementedException";
        }
    }
}