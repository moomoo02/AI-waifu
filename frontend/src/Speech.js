import env from "react-dotenv";

console.log(env.SPEECH_KEY);
var sdk = require("microsoft-cognitiveservices-speech-sdk");

// This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
const speechConfig = sdk.SpeechConfig.fromSubscription(env.SPEECH_KEY, env.SPEECH_REGION);

// The language of the voice that speaks.
speechConfig.speechSynthesisVoiceName = "en-US-JennyNeural"; 

// Create the speech synthesizer.
var synthesizer = new sdk.SpeechSynthesizer(speechConfig);

//Start the synthesizer and wait for a result.

const speak = async (text, fn) => {
  var byteData = 0;

  synthesizer.speakTextAsync(text,
    function (result) {
    if (result.reason === sdk.ResultReason.SynthesizingAudioCompleted) {
      console.log("synthesis finished.");
    } else {
      console.error("Speech synthesis canceled, " + result.errorDetails +
          "\nDid you set the speech resource key and region values?");
    }
    
    synthesizer.close();
    synthesizer = null;

    byteData = result.audioData;
    fn(byteData);
    },
    function (err) {
      console.trace("err - " + err);
      synthesizer.close();
      synthesizer = null;
    });


};

export const useTTS = async (text) => {
  speak(text, function (byteData) {
    console.log(byteData);
    console.log(byteData.byteLength)
  });
}

// useTTS();
// playByteArray(bytes);

