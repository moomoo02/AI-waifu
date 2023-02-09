var sdk = require("microsoft-cognitiveservices-speech-sdk");
const path = require('path')
require('dotenv').config({ path: path.resolve(__dirname, '../.env') })

class Speech
{
  #synthesizer;
  audioFile = "YourAudioFile.wav";
  constructor(){

    // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
    const speechConfig = sdk.SpeechConfig.fromSubscription(process.env.SPEECH_KEY, process.env.SPEECH_REGION);
    const audioConfig = sdk.AudioConfig.fromAudioFileOutput(this.audioFile);
    // The language of the voice that speaks.
    speechConfig.speechSynthesisVoiceName = "en-US-JennyNeural";

    // Create the speech synthesizer.
    this.synthesizer = new sdk.SpeechSynthesizer(speechConfig, audioConfig);
  }

  speak(text){
    var rl = readline.createInterface({
      input: process.stdin,
      output: process.stdout
    });

    //Start the synthesizer and wait for a result.
    this.synthesizer.speakTextAsync(text,
      function (result) 
      {
        if (result.reason === sdk.ResultReason.SynthesizingAudioCompleted) {
          console.log("synthesis finished.");
        } else {
          console.error("Speech synthesis canceled, " + result.errorDetails +
              "\nDid you set the speech resource key and region values?");
        }
        this.synthesizer.close();
        this.synthesizer = null;
      },
      function (err) 
      {
        console.trace("err - " + err);
        this.synthesizer.close();
        this.synthesizer = null;
      }
    );
    console.log("Now synthesizing to: " + this.audioFile);
  }


}

const speech = new Speech();
speech.speak("Hello");
