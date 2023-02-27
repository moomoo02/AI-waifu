using System.Diagnostics;
using backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using OpenAI_API;
using OpenAI_API.Completions;

namespace Backend.Controllers
{
    [ApiController]
    [Route("text-speech")]
    public class TextToSpeechController : ControllerBase
    {
        static private SpeechConfig speechConfig;
        static private AudioConfig audioConfig;
        public TextToSpeechController(){
            // The language of the voice that speaks.
            DotNetEnv.Env.Load();
            string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
            string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

            TextToSpeechController.speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
            TextToSpeechController.speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural"; 
            
            TextToSpeechController.audioConfig =  AudioConfig.FromDefaultSpeakerOutput(); 

        }
        //GET /text-speech/prompt
        [HttpGet("{text}")]
        public async Task<ActionResult<SpeechDto>> GetSpeech(String text)
        {
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, audioConfig))
            {
                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);

                return OutputSpeechSynthesisResult(speechSynthesisResult, text);
            }
        }
        
        //Helper Function to output speech
        private ActionResult<SpeechDto> OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    Console.WriteLine($"Speech synthesized for text: [{text}]");

                    SpeechDto result = new SpeechDto()
                    {
                      data = speechSynthesisResult.AudioData,
                      error = ""
                    };

                    return Ok(result);
                    
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);

                    result = new SpeechDto()
                    {
                      error = $"CANCELED: Reason={cancellation.Reason}\n"
                    };

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        result.error += $"CANCELED: ErrorCode={cancellation.ErrorCode}\n";
                        result.error += $"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]";
                        result.error += $"CANCELED: Did you set the speech resource key and region values?";
                    }

                    return NotFound(result);
                default:
                    return NotFound(speechSynthesisResult.AudioData);
            }
        }
    }
}
