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
        public TextToTextController(OpenAIAPI openAiApi){
            this.api = openAiApi;
        }
        //GET /text-text
        [HttpGet]
        public ActionResult<string> GetText()
        {
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = @"Decide whether a Tweet's sentiment is positive, neutral, or negative.
                                        Tweet: I hated the new Batman movie!
                                        Sentiment:";
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;

            var completion = api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Result.Completions)
            {
                OutputMsg += comp.Text;
            }
            return Ok(OutputMsg);
        }
    }
}