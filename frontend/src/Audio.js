import React, { useState, useEffect } from "react";

export const useAudio = url => {
  const [audio] = useState(new Audio(url));
  const [playing, setPlaying] = useState(false);

  const toggle = () => setPlaying(!playing);

  useEffect(() => {
      playing ? audio.play() : audio.pause();
      if(playing){
        console.log("Playing " + url);
      }
    },
    [playing]
  );

  useEffect(() => {
    audio.addEventListener('ended', () => setPlaying(false));
    return () => {
      audio.removeEventListener('ended', () => setPlaying(false));
    };
  }, []);

  return [playing, toggle];
};

export const Player = ({ url }) => {
  const [playing, toggle] = useAudio(url);

  return (
    <div>
        hello
      <button onClick={toggle}>{playing ? "Pause" : "Play"}</button>
    </div>
  );
};

