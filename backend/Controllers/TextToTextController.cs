using System.Diagnostics;
using backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace Backend.Controllers
{
    [ApiController]
    [Route("text-text")]
    public class TextToTextController : ControllerBase
    {
        private OpenAIAPI api;
        static private string BasePrompt = System.IO.File.ReadAllText(Environment.CurrentDirectory + "/Prompts/TsunderePrompt.txt");
        public TextToTextController(OpenAIAPI openAiApi){
            this.api = openAiApi;
            //Create a completion
        }
        //GET /text-text
        [HttpGet]
        public ActionResult<MessageDto> GetText()
        {
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = BasePrompt;
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;

            var completion = api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Result.Completions)
            {
                OutputMsg += comp.Text;
            }

            MessageDto response = new MessageDto()
            {
                direction = "incoming",
                content = OutputMsg,
            };

            return Ok(response);
        }
        //GET /text-text/prompt
        [HttpGet("{prompt}")]
        public ActionResult<MessageDto> GetText(string prompt)
        {
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = BasePrompt + prompt + " Friend: ";
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 100;

            var completion = api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Result.Completions)
            {
                OutputMsg += comp.Text;
            }
            
            //Trim beginning new lines
            string CleanedOutputMsg = OutputMsg.TrimStart('\r', '\n');

            MessageDto response = new MessageDto()
            {
                direction = "incoming",
                content = CleanedOutputMsg,
            };
            
            //Update basePrompt
            BasePrompt += prompt + " Friend: " + CleanedOutputMsg + " Protagonist: ";
            return Ok(response);
        }
    }
}