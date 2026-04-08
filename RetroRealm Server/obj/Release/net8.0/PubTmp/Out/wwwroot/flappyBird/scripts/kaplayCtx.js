import kaplay from "https://unpkg.com/kaplay@3001.0.19/dist/kaplay.mjs";

// Kaplay engine init
const k = kaplay({
    width: 143,
    height: 256,
    letterbox: true,
    scale: 8,
    filter: "nearest",
    background: "#000000",
});

export default k;
