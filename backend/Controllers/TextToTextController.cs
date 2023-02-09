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
        static private string SentinentAnalysisPrompt = System.IO.File.ReadAllText(Environment.CurrentDirectory + "/Prompts/SentimentAnalysis.txt");
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
        public ActionResult<MessageDto> GetResponse(string prompt)
        {
            // Get Response message
            string contentMessage = GetText(prompt);

            // Sentiment Analysis on text
            string emotionMessage = GetEmotion(contentMessage);

            MessageDto response = new MessageDto()
            {
                direction = "incoming",
                content = contentMessage,
                emotion = emotionMessage
            };
            
            //Update basePrompt
            //BasePrompt += prompt + " Friend: " + contentMessage + " Protagonist: ";
            return Ok(response);
        }
        // Sentiment Analysis on text
        private String GetEmotion(string text)
        {
            // Get Emotion
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = SentinentAnalysisPrompt + text;
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 16;

            var completion = api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Result.Completions)
            {
                OutputMsg += comp.Text;
            }
            
            // Trim beginning new lines
            string CleanedOutputMsg = OutputMsg.TrimStart('\r', '\n');

            return CleanedOutputMsg;
        }
        // Text to Text 
        private string GetText(string prompt)
        {
            // Get Response message
            string OutputMsg = string.Empty;
            CompletionRequest completionRequest = new CompletionRequest();
            completionRequest.Prompt = BasePrompt + prompt + "\nWaifu: ";
            completionRequest.Model= OpenAI_API.Models.Model.DavinciText;
            completionRequest.MaxTokens = 100;

            var completion = api.Completions.CreateCompletionAsync(completionRequest);

            foreach (var comp in completion.Result.Completions)
            {
                OutputMsg += comp.Text;
            }
            
            //Trim beginning new lines
            string CleanedOutputMsg = OutputMsg.TrimStart('\r', '\n');

            return CleanedOutputMsg;        
        }
    }
}