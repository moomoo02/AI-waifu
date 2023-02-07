import logo from './logo.svg';
import './App.css';
import * as PIXI from 'pixi.js';
import { Live2DModel } from 'pixi-live2d-display/cubism4';
import React, {useState, useEffect} from 'react';
import axios from 'axios';
import Chat from './Chat'

// expose PIXI to window so that this plugin is able to
// reference window.PIXI.Ticker to automatically update Live2D models
window.PIXI = PIXI;


function App() {

  const [data, setData] = useState(null);
  const CanvasContainerElement = document.getElementById('canvas-container')

  axios.get('/chat').then((response) => {
    const dataStr = JSON.stringify(response.data);
    setData(dataStr);
  })

  useEffect(() => {
    const app = new PIXI.Application({
      view: document.getElementById("canvas"),
      autoStart: true,
      resizeTo: CanvasContainerElement,
      backgroundColor: 0x333333
    });

    Live2DModel.from("resources/runtimeb/mao_pro_t02.model3.json", {
      idleMotionGroup: "Idle"
    }).then((model) => {
      app.stage.addChild(model);
      model.anchor.set(0.5, 0.5); //Center model

      function resizeWaifu() {
        model.scale.set(0.25);
        model.x = (CanvasContainerElement.offsetWidth + 0) / 2;
        model.y = (CanvasContainerElement.offsetHeight + 1000) / 2;
      }
      resizeWaifu();

      model.on("pointertap", () => {
        model.motion("Tap@Body");
      });
    });
  }, []);


  return (
  <div class='app'>
  <div id='canvas-container' class='child waifu-container'>
    <canvas id="canvas" />
  </div>
  <div class='child chat-container'>
    <Chat />
  </div>
  </div>
  
  );
}

export default App;
