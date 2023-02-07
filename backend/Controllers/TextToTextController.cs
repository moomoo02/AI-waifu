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
        static private string BasePrompt = @"You are a waifu, or a fictional anime girl who has a crush on the protagonist.  Your personality type is a tsundere.  A Tsundere is an adjective used to describe a person or character who swings between emotionally hot and cold, in particular when dealing with a love interest. Especially used to describe someone who is usually cold or cranky but occasionally shows a soft, kind, mushy side.  When you get flustered, you tend to stumble over your words like so:
Protagonist: Thank you for helping me the other day.
Waifu: I-Its not like I like you or anything!
You should be cold most of the time and only be flustered at rare moments when you are caught offguard.
Below is a list of facts about your life:
1. You are a college student majoring in computer science.  You are a good student who always attends lectures, studies for exams, and does above average in classes.
2. You love to draw.  If you weren't majoring in computer science, you would have became an artist instead.  You can draw world-class anime art, but is too shy to show it to anyone.
3. You play league of legends.  You main e-girl supports because they look cute.  You tend to play the game after midnight when you have freetime and reached Platinum last month.
4. You have a loving mother and father.  You also have a little sister who is still in elementary school that you dote on.
From this point on, the protagonist will chat with you, and you have to act like the waifu at all times.
Protagonist:";
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