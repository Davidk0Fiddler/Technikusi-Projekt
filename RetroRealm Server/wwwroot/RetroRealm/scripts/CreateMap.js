import k from "./kaplayCtx.js";
import { dirtSprites, grassSprites, waterSprites } from "./sprites.js";
import { dirtLayer, grassLayer, objectsLayer } from "./map_layers.js";

function getRandomDirt() {
    let dirts = ["dirt1", "dirt2", "dirt3"]

    let dirtName = dirts[Math.floor(Math.random()*dirts.length)];

    return dirtName;
}

function getRandomGrass() {
    let grasses = ["grass1", "grass2", "grass3"]

    let grassName = grasses[Math.floor(Math.random()*grasses.length)];

    return grassName;
}

export function CreateMap() {
    const dirtmap = k.addLevel(dirtLayer, 
    {
        tileWidth: 32,
        tileHeight: 32,
        tiles: {
            "a": () => [
                sprite(getRandomDirt()),
                z(1),
            ],
            "b": () => [
                sprite("waterLayerOuterEmptyTopLeft"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],
            "c": () => [
                sprite("waterLayerOuterEmptyTopCenter"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],
            "d": () => [
                sprite("waterLayerOuterEmptyTopRight"),
                z(1),
            ],
            "e": () => [
                sprite("waterLayerOuterEmptyCenterLeft"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],
            "f": () => [
                sprite("waterLayerOuterEmptyCenterRight"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],
            "g": () => [
                sprite("waterLayerOuterEmptyBottomLeft"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],
            "h": () => [
                sprite("waterLayerOuterEmptyBottomCenter"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],
            "i": () => [
                sprite("waterLayerOuterEmptyBottomRight"),
                z(1),
                "object",
                area(),
                body({ isStatic: true }),
            ],


        }
    });

    const grassmap = k.addLevel(grassLayer, 
    {
        tileWidth: 32,
        tileHeight: 32,
        tiles: {
            "a": () => [
                sprite(getRandomGrass()),
                z(2),
            ],
            "b": () => [
                sprite("grassLayerOuterEmptyTopLeft"),
                z(2),
            ],
            "c": () => [
                sprite("grassLayerOuterEmptyTopCenter"),
                z(2),
            ],
            "d": () => [
                sprite("grassLayerOuterEmptyTopRight"),
                z(2),
            ],
            "e": () => [
                sprite("grassLayerOuterEmptyCenterLeft"),
                z(2),
            ],
            "f": () => [
                sprite("grassLayerOuterEmptyCenterRight"),
                z(2),
            ],
            "g": () => [
                sprite("grassLayerOuterEmptyBottomLeft"),
                z(2),
            ],
            "h": () => [
                sprite("grassLayerOuterEmptyBottomCenter"),
                z(2),
            ],
            "i": () => [
                sprite("grassLayerOuterEmptyBottomRight"),
                z(2),
            ],
            "k": () => [
                sprite("grassLayerInnerEmptyTopLeft"),
                z(2),
            ],
            "l": () => [
                sprite("grassLayerInnerEmptyTopRight"),
                z(2),
            ],
            "m": () => [
                sprite("grassLayerInnerEmptyBottomLeft"),
                z(2),
            ],
            "n": () => [
                sprite("grassLayerInnerEmptyBottomRight"),
                z(2),
            ],
            
        }
    });

    const objectmap = k.addLevel(objectsLayer,
    {
        tileWidth: 32,
        tileHeight: 32,
        tiles: {
            "r": () => [
                sprite("rock"),
                z(3),
                scale(1.5),
                "object",
                area(),
                body({ isStatic: true }),

            ],
            "b": () => [
                sprite("barrel"),
                z(3),
                area(),
                body({ isStatic: true }),
                "object",
            ],
            "c": () => [
                sprite("bucket"),
                z(3),
            ],
            "d": () => [
                area({shape: new k.Rect( k.vec2(0, 0), k.vec2(32, 32), ), }),
                body({ isStatic: true }),
                z(3),
                "object",
            ],
        }
    });
}