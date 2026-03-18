import kaplay from "https://unpkg.com/kaplay@3001.0.19/dist/kaplay.mjs";

var k = kaplay({
    width: 1536,
    height: 1024,
    letterbox: true,
    scale: 3,
    filter: "nearest",
    background: "#000000",
    debug: true,
});

export default k;