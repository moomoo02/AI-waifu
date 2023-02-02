import logo from './logo.svg';
import './App.css';
import * as PIXI from 'pixi.js';
import { Live2DModel } from 'pixi-live2d-display/cubism4';
import {useEffect} from 'react'

// expose PIXI to window so that this plugin is able to
// reference window.PIXI.Ticker to automatically update Live2D models
window.PIXI = PIXI;


function App() {

  useEffect(() => {
    const app = new PIXI.Application({
      view: document.getElementById("canvas"),
      autoStart: true,
      resizeTo: window
    });

    Live2DModel.from("resources/runtimeb/mao_pro_t02.model3.json", {
      idleMotionGroup: "Idle"
    }).then((model) => {
      console.log(model)
      app.stage.addChild(model);
      model.anchor.set(0.5, 0.5);
      model.position.set(window.innerWidth / 4, window.innerHeight / 4);
      model.scale.set(0.09, 0.09);

      function resizeWaifu() {
        model.scale.set(window.innerHeight / 1280 * .8);
        model.x = (window.innerWidth) / 2;
        model.y = (window.innerHeight + 2000) / 2;
      }
      resizeWaifu();

      model.on("pointertap", () => {
        model.motion("Tap@Body");
      });
    });
  }, []);


  return <canvas id="canvas" />;
}

export default App;
