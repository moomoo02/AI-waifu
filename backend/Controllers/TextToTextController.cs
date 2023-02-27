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
        static private string BasePrompt = backend.Prompts.BasePrompt;
        static private string SentinentAnalysisPrompt = backend.Prompts.SentinentAnalysisPrompt;
        public TextToTextController(OpenAIAPI openAiApi){
            this.api = openAiApi;
        }
        //GET /text-text
        [HttpGet]
        public async Task<ActionResult<MessageDto>> GetText()
        {
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = BasePrompt;
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;

            var completion = await api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Completions)
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
        public async Task<ActionResult<MessageDto>> GetResponseAsync(string prompt)
        {
            // Get Response message
            string contentMessage = await GetTextAsync(prompt);

            // Sentiment Analysis on text
            string emotionMessage = await GetEmotionAsync(contentMessage);

            MessageDto response = new MessageDto()
            {
                direction = "incoming",
                content = contentMessage,
                emotion = emotionMessage,
            };
            
            //Update basePrompt
            BasePrompt += prompt + "\nFriend: " + contentMessage + "\nProtagonist: ";
            return Ok(response);
        }
        // Sentiment Analysis on text
        private async Task<String> GetEmotionAsync(string text)
        {
            // Get Emotion
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = SentinentAnalysisPrompt + text;
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 16;

            var completion = await api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Completions)
            {
                OutputMsg += comp.Text;
            }
            
            // Trim beginning new lines
            string CleanedOutputMsg = OutputMsg.TrimStart('\r', '\n');

            return CleanedOutputMsg;
        }
        // Text to Text 
        private async Task<string> GetTextAsync(string prompt)
        {
            // Get Response message
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = BasePrompt + prompt + "\nWaifu: ";
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 100;

            var completion = await api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Completions)
            {
                OutputMsg += comp.Text;
            }
            
            //Trim beginning new lines
            string CleanedOutputMsg = OutputMsg.TrimStart('\r', '\n');

            return CleanedOutputMsg;        
        }
    }
}