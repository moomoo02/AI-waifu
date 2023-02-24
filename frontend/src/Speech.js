var sdk = require("microsoft-cognitiveservices-speech-sdk");
const path = require('path')
require('dotenv').config({ path: path.resolve(__dirname, '../.env') })

// class Speech
// {
//   #synthesizer;
//   audioFile = "YourAudioFile.wav";
//   constructor(){

//     // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
//     const speechConfig = sdk.SpeechConfig.fromSubscription(process.env.SPEECH_KEY, process.env.SPEECH_REGION);
//     const audioConfig = sdk.AudioConfig.fromAudioFileOutput(this.audioFile);
//     // The language of the voice that speaks.
//     speechConfig.speechSynthesisVoiceName = "en-US-JennyNeural";

//     // Create the speech synthesizer.
//     this.synthesizer = new sdk.SpeechSynthesizer(speechConfig, audioConfig);
//   }

//   getSynthesizer(){
//     return this.synthesizer;
//   }

//   speak(text){
//     //Start the synthesizer and wait for a result.
//     this.synthesizer.speakTextAsync(text,
//       function (result) 
//       {
//         console.log("Bytes:" + result.audioData)
//         if(result.reason === sdk.ResultReason.SynthesizingAudioStarted){
//           console.log("Synthesis has begun");
//         }else{
//           console.log("Synthesis has not started");
//         }

//         if (result.reason === sdk.ResultReason.SynthesizingAudioCompleted) {
//           console.log("synthesis finished.");
//         } else {
//           console.error("Speech synthesis canceled, " + result.errorDetails +
//               "\nDid you set the speech resource key and region values?");
//         }
//         this.synthesizer.close();
//         this.synthesizer = null;
//       },
//       function (err) 
//       {
//         console.trace("err - " + err);
//         this.synthesizer.close();
//         this.synthesizer = null;
//       }
//     );
//     console.log("Now synthesizing to: " + this.audioFile);
//   }


// }

// const text = "Hello";
// const speech = new Speech();
// speech.speak(text);
 



var sdk = require("microsoft-cognitiveservices-speech-sdk");
var readline = require("readline");

var audioFile = "YourAudioFile.wav";
// This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
const speechConfig = sdk.SpeechConfig.fromSubscription(process.env.SPEECH_KEY, process.env.SPEECH_REGION);
const audioConfig = sdk.AudioConfig.fromAudioFileOutput(audioFile);

// The language of the voice that speaks.
speechConfig.speechSynthesisVoiceName = "en-US-JennyNeural"; 

// Create the speech synthesizer.
var synthesizer = new sdk.SpeechSynthesizer(speechConfig, audioConfig);

const text = "whats up I'm moomoo02";
// Start the synthesizer and wait for a result.

export const speak = (text) => {
  synthesizer.speakTextAsync(text,
    function (result) {
    if (result.reason === sdk.ResultReason.SynthesizingAudioCompleted) {
      console.log("synthesis finished.");
      return result.audioData;
    } else {
      console.error("Speech synthesis canceled, " + result.errorDetails +
          "\nDid you set the speech resource key and region values?");
    }
    synthesizer.close();
    synthesizer = null;
    },
    function (err) {
      console.trace("err - " + err);
      synthesizer.close();
      synthesizer = null;
    });
  console.log("Now synthesizing to: " + audioFile);

};
