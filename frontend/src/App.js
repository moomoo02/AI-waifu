import logo from './logo.svg';
import './App.css';
import * as PIXI from 'pixi.js';
import { Live2DModel } from 'pixi-live2d-display/cubism4';
import React, {useState, useEffect, useRef} from 'react';
import axios from 'axios';
import Chat from './Chat'
import { Button } from '@chatscope/chat-ui-kit-react';

// expose PIXI to window so that this plugin is able to
// reference window.PIXI.Ticker to automatically update Live2D models
window.PIXI = PIXI;

const expressions = {"Neutral": 1, "Happy": 2, "Smug": 3, "Excited": 4, "Sad": 5, "Embarassed": 6, "Scared": 7, "Annoyed": 8};


function App() {

  const [model, setModel] = useState();
  const CanvasContainerElement = useRef(null);
  const [scale, setScale] = useState(0.25);
  const [emotion, setEmotion] = useState("Neutral");

  useEffect(() => {

    console.log("UseEffect " + CanvasContainerElement.current);
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
      console.log("HERE");
      
      console.log("Canvas width = " + CanvasContainerElement.current.offsetWidth); //270
      console.log("Canvas height = " + CanvasContainerElement.current.offsetHeight); //572

      model.scale.set(scale);
      // model.x = (270 + 0) / 2;
      // model.y = (572 + 1000) / 2;
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

  // useEffect(() => {
  //   function resizeWaifu() {
  //     model.scale.set(scale);
  //     console.log("Canvas width = " + CanvasContainerElement.offsetWidth);
  //     model.x = (CanvasContainerElement.offsetWidth + 0) / 2;
  //     model.y = (CanvasContainerElement.offsetHeight + 1000) / 2;
  //   }
  //   resizeWaifu();
  // }, [model, CanvasContainerElement])

  function handleButton(id)
  {
    console.log("Button")
    model.expression("exp_0" + id);
  }

  function handleButtonMotion(id)
  {
    model.motion("Tap@Body");
  }
  
  function getRandomInt(max) {
    return Math.floor(Math.random() * max);
  }
  return (
  <div class='app'>

    <div ref={CanvasContainerElement} id='canvas-container' class='child waifu-container'>
      <canvas id="canvas" />
    </div>

  <div class='child chat-container'>
    <Chat setEmotion={setEmotion}/>
  </div>
  {Object.keys(expressions).map((expression) => <Button onClick={() => handleButton(expressions[expression])}>{expression} </Button>)}
 {/* .map((key,val) => <Button onClick={() => handleButton(key)}> Exp {val} </Button>)} */}
  <Button onClick={handleButtonMotion}> Motion 1 </Button>
  </div>
  
  );
}

export default App;
