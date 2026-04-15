import kaplay from "https://unpkg.com/kaplay@3001.0.19/dist/kaplay.mjs";
const k = kaplay({
  width: 640,
  height: 640,
  letterbox: true,
  debug: false,
  background: [0, 0, 0],
  maxFPS: 60,
});

export default k;
