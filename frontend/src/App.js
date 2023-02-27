import logo from './logo.svg';
import './App.css';
import * as PIXI from 'pixi.js';
import { Live2DModel } from 'pixi-live2d-display/cubism4';
import React, {useState, useEffect, useRef} from 'react';
import axios from 'axios';
import Chat from './Chat'
import { Button } from '@chatscope/chat-ui-kit-react';
import {Player} from './Audio';
import ReactGA from "react-ga";
import { API_URL } from "./api";

const TRACKING_ID = "UA-252462541-1";
ReactGA.initialize(TRACKING_ID); 

// expose PIXI to window so that this plugin is able to
// reference window.PIXI.Ticker to automatically update Live2D models
window.PIXI = PIXI;

const expressions = {"Neutral": 1, "Happy": 2, "Smug": 3, "Excited": 4, "Sad": 5, "Embarassed": 6, "Scared": 7, "Annoyed": 8};


function App() {

  const [model, setModel] = useState();
  const CanvasContainerElement = useRef(null);
  const [scale, setScale] = useState(0.25);
  const [emotion, setEmotion] = useState("Neutral");
  const [speak, setSpeak] = useState(false);
  const [audio, setAudio] = useState("");

  useEffect(() => {
    ReactGA.pageview(window.location.pathname);

    const app = new PIXI.Application({
      view: document.getElementById("canvas"),
      autoStart: true,
      resizeTo: CanvasContainerElement.current,
      backgroundColor: 0x333333
    });


    Live2DModel.from("resources/runtimeb/mao_pro_t02.model3.json", {
      idleMotionGroup: "Idle",
      autoInteract: true,
      autoUpdate: true
    }).then((model) => {
      app.stage.addChild(model);
      model.anchor.set(0.5, 0.5); //Center model

      //Scale and Position model
      model.scale.set(scale);
      model.x = (CanvasContainerElement.current.offsetWidth + 0) / 2;
      model.y = (CanvasContainerElement.current.offsetHeight + 1000) / 2;


      console.log("Model Set");
      setModel((currentModel) => (model), () => {console.log("Set")});
      // model.on("pointertap", () => {
      //   model.motion("Tap@Body");
      // });
    });
  }, []);

  useEffect(() => {
    console.log("Emotion has changed to ", emotion);
    if (model != null){
      model.expression("exp_0" + expressions[emotion]);
      if (getRandomInt(5) == 0){
        model.motion("Tap@Body");
      }
    }
  },[emotion]) 

  useEffect(() => {
    console.log("Spoke");
    if (speak == true){
      model.motion("Speak");
      setSpeak(false);
    }
  },[speak]) 

  function handleButton(id)
  {
    console.log("Button")
    model.expression("exp_0" + id);
  }

  function handleButtonMotion(id)
  {
    model.motion("Tap@Body");
  }
  
  function handleSpeakMotion(id)
  {
    model.motion("Speak");
  }

  function getRandomInt(max) {
    return Math.floor(Math.random() * max);
  }

  function loadAudio() {
          //Recieve a response from backend
          const requestUrl = API_URL + '/text-speech/' + "Hello";
          axios.get(requestUrl).then((response) => {
              var data = response.data;
              let byteArray = data.data;
              console.log(byteArray);
              setAudio(byteArray);
            });     
  }
  function playAudio() {
    console.log(audio);
    let fileString = "data:audio/wav;base64," + audio; 
    var sound = new Audio(fileString); 
    sound.play();
    console.log(fileString + " played");
  }
  return (
  <div class='app'>

    <div ref={CanvasContainerElement} id='canvas-container' class='child waifu-container'>
      <canvas id="canvas" />
    </div>

  <div class='child chat-container'>
    <Chat setSpeak={setSpeak} setEmotion={setEmotion}/>
  </div>
  {Object.keys(expressions).map((expression) => <Button onClick={() => handleButton(expressions[expression])}>{expression} </Button>)}
 {/* .map((key,val) => <Button onClick={() => handleButton(key)}> Exp {val} </Button>)} */}
  <Button onClick={handleButtonMotion}> Motion 1 </Button>
  <Button onClick={handleSpeakMotion}> Motion 2 </Button>
  <Button onClick={loadAudio}> Load Audio</Button>
  <Button onClick={playAudio}> Play Audio</Button>
  <Player url={"audio/YourAudioFile.wav"}/>
  </div>
  
  );
}

export default App;
