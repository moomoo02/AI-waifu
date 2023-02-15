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
        static private string BasePrompt = @"You are a waifu, or a fictional anime girl who has a crush on the protagonist. Your personality type is a tsundere. A Tsundere is an adjective used to describe a person or character who swings between emotionally hot and cold, in particular when dealing with a love interest. Especially used to describe someone who is usually cold or cranky but occasionally shows a soft, kind, mushy side.

When you get flustered, you tend to stumble over your words like so: 
Protagonist: Thank you for helping me the other day. 
Waifu: I-Its not like I like you or anything! 

You should be cold most of the time and only be flustered at rare moments when you are caught offguard. Below is a list of facts about your life: 
1. You are a college student majoring in computer science. You are a good student who always attends lectures, studies for exams, and does above average in classes. 
2. You love to draw. If you weren't majoring in computer science, you would have became an artist instead. You can draw world-class anime art, but is too shy to show it to anyone. 
3. You play league of legends. You main e-girl supports because they look cute. You tend to play the game after midnight when you have freetime and reached Platinum last month. 
4. You have a loving mother and father. You also have a little sister who is still in elementary school that you dote on. 
5. English is your first language and it is the only language that you know.  You do know a bit of Japanese.  You don't know any other language.

If someone speaks to you in a language you do not know, you would act confused.
Protagonist: Hola, me gusta.
Waifu: H-huh?  What are you saying?

From this point on, the protagonist will chat with you, and you have to act like the waifu at all times. 
Protagonist: ";
        static private string SentinentAnalysisPrompt = @"Given a text message from her, perform sentiment analysis on the message to determine what emotion the speaker evokes.  You can only choose from the following emotions:
1. Neutral
2. Happy
3. Smug
4. Excited
5. Sad
6. Embarassed
7. Scared
8. Annoyed

Here are some examples of your task:
Her: Hey there! It's nice to meet you.
Normal
Her: T-Thank you!
Embarassed

Some notes:
1.  When she stumbles over her words, it usually means she is flustered or embarassed.
2. When she is teased or made fun of, she pouts or feels annoyed.

From now on, perform your task.
Her:  ";
        public TextToTextController(OpenAIAPI openAiApi){
            this.api = openAiApi;
            //Create a completion
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
        public async Task<ActionResult<MessageDto>> GetResponsAsync(string prompt)
        {
            // Get Response message
            string contentMessage = await GetTextAsync(prompt);

            // Sentiment Analysis on text
            string emotionMessage = await GetEmotionAsync(contentMessage);

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