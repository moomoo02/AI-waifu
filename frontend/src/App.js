import logo from './logo.svg';
import './App.css';
import * as PIXI from 'pixi.js';
import { Live2DModel } from 'pixi-live2d-display/cubism4';
import React, {useState, useEffect} from 'react';
import axios from 'axios';

// expose PIXI to window so that this plugin is able to
// reference window.PIXI.Ticker to automatically update Live2D models
window.PIXI = PIXI;


function App() {

  const [data, setData] = useState(null);

  axios.get('/api').then((response) => {
    setData(response.data.message);
  })

  useEffect(() => {
    const app = new PIXI.Application({
      view: document.getElementById("canvas"),
      autoStart: true,
      resizeTo: window,
      backgroundColor: 0x333333
    });

    Live2DModel.from("resources/runtimeb/mao_pro_t02.model3.json", {
      idleMotionGroup: "Idle"
    }).then((model) => {
      app.stage.addChild(model);
      model.anchor.set(0.5, 0.5);
      model.position.set(window.innerWidth / 4, window.innerHeight / 4);

      function resizeWaifu() {
        model.scale.set(window.innerHeight / 1280 * .5);
        model.x = (window.innerWidth) / 2;
        model.y = (window.innerHeight + 1200) / 2;
      }
      resizeWaifu();

      model.on("pointertap", () => {
        model.motion("Tap@Body");
      });
    });
  }, []);


  return (
  <div>
  <canvas id="canvas" />
  <h1>{data}</h1>
  </div>
  
  );
}

export default App;
