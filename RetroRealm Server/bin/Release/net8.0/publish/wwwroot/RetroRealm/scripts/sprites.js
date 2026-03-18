import k from "./kaplayCtx.js";

const TILE_SIZE = 32

export const dirtSprites = k.loadSpriteAtlas("/RetroRealm/assets/tiles/dirt.png", {
    "dirt1": { x: 0, y: 160, width: TILE_SIZE, height: TILE_SIZE, },
    "dirt2": { x: 32, y: 160, width: TILE_SIZE, height: TILE_SIZE, },
    "dirt3": { x: 64, y: 160, width: TILE_SIZE, height: TILE_SIZE, },
});

export const grassSprites = k.loadSpriteAtlas("/RetroRealm/assets/tiles/grass.png", {
    "grass1": { x: 0, y: 160, width: TILE_SIZE, height: TILE_SIZE, },
    "grass2": { x: 32, y: 160, width: TILE_SIZE, height: TILE_SIZE, },
    "grass3": { x: 64, y: 160, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerInnerEmptyTopLeft" : { x: 32, y: 0, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerInnerEmptyTopRight": { x: 64, y: 0, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerInnerEmptyBottomLeft": { x: 32, y: 32, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerInnerEmptyBottomRight": { x: 64, y: 32, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyTopLeft": { x: 0, y: 64, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyTopCenter": { x: 32, y: 64, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyTopRight": { x: 64, y: 64, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyCenterLeft": { x: 0, y: 96, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyCenterRight": { x: 64, y: 96, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyBottomLeft": { x: 0, y: 128, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyBottomCenter": { x: 32, y: 128, width: TILE_SIZE, height: TILE_SIZE, },
    "grassLayerOuterEmptyBottomRight": { x: 64, y: 128, width: TILE_SIZE, height: TILE_SIZE, },
});

export const waterSprites = k.loadSpriteAtlas("/RetroRealm/assets/tiles/watergrass.png", {
    "waterLayerOuterEmptyTopLeft": { x: 64, y: 32, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyTopCenter": { x: 32, y: 64, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyTopRight": { x: 32, y: 32, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyCenterLeft": { x: 0, y: 96, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyCenterRight": { x: 64, y: 96, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyBottomLeft": { x:64, y: 0, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyBottomCenter": { x: 32, y: 128, width: TILE_SIZE, height: TILE_SIZE, },
    "waterLayerOuterEmptyBottomRight": { x: 32, y: 0, width: TILE_SIZE, height: TILE_SIZE, },
});

export const rockSprites = k.loadSpriteAtlas("/RetroRealm/assets/tiles/rock.png", {
    "rock": { x: 0, y: 0, width: TILE_SIZE, height: TILE_SIZE}
});


export const barrelSprites = k.loadSpriteAtlas("/RetroRealm/assets/tiles/barrel.png", {
    "barrel": { x: 32, y: 26, width: TILE_SIZE, height: 40}
});

export const bucketSprites = k.loadSpriteAtlas("/RetroRealm/assets/tiles/buckets.png", {
    "bucket": { x: 0, y: 0, width: TILE_SIZE, height: TILE_SIZE}
});


export const baseCharacterSprite = k.loadSprite("baseCharacter", "/RetroRealm/assets/sprites/base.png",
    {
        sliceX: 4,
        sliceY: 4,
        anims: {
            idleUp:    { from: 1,  to: 1,  loop: true, speed: 1 },
            walkUp:    { from: 0,  to: 3,  loop: true, speed: 8 },

            idleRight: { from: 5,  to: 5,  loop: true, speed: 1 },
            walkRight: { from: 4,  to: 7,  loop: true, speed: 8 },

            idleDown:  { from: 9,  to: 9,  loop: true, speed: 1 },
            walkDown:  { from: 8,  to: 11,  loop: true, speed: 8 },

            idleLeft:  { from: 13, to: 13, loop: true, speed: 1 },
            walkLeft:  { from: 12,  to: 15, loop: true, speed: 8 },
        },
    }
);

export const healerCharacterSprite = k.loadSprite("healerCharacter", "/RetroRealm/assets/sprites/healer.png",
    {
        sliceX: 4,
        sliceY: 4,
        anims: {
            idleUp:    { from: 1,  to: 1,  loop: true, speed: 1 },
            walkUp:    { from: 0,  to: 3,  loop: true, speed: 8 },

            idleRight: { from: 5,  to: 5,  loop: true, speed: 1 },
            walkRight: { from: 4,  to: 7,  loop: true, speed: 8 },

            idleDown:  { from: 9,  to: 9,  loop: true, speed: 1 },
            walkDown:  { from: 8,  to: 11,  loop: true, speed: 8 },

            idleLeft:  { from: 13, to: 13, loop: true, speed: 1 },
            walkLeft:  { from: 12,  to: 15, loop: true, speed: 8 },
        },
    }
);

export const ninjaCharacterSprite = k.loadSprite("ninjaCharacter", "/RetroRealm/assets/sprites/ninja.png",
    {
        sliceX: 4,
        sliceY: 4,
        anims: {
            idleUp:    { from: 1,  to: 1,  loop: true, speed: 1 },
            walkUp:    { from: 0,  to: 3,  loop: true, speed: 8 },

            idleRight: { from: 5,  to: 5,  loop: true, speed: 1 },
            walkRight: { from: 4,  to: 7,  loop: true, speed: 8 },

            idleDown:  { from: 9,  to: 9,  loop: true, speed: 1 },
            walkDown:  { from: 8,  to: 11,  loop: true, speed: 8 },

            idleLeft:  { from: 13, to: 13, loop: true, speed: 1 },
            walkLeft:  { from: 12,  to: 15, loop: true, speed: 8 },
        },
    }
);

export const oldCharacterSprite = k.loadSprite("oldCharacter", "/RetroRealm/assets/sprites/old.png",
    {
        sliceX: 4,
        sliceY: 4,
        anims: {
            idleUp:    { from: 1,  to: 1,  loop: true, speed: 1 },
            walkUp:    { from: 0,  to: 3,  loop: true, speed: 8 },

            idleRight: { from: 5,  to: 5,  loop: true, speed: 1 },
            walkRight: { from: 4,  to: 7,  loop: true, speed: 8 },

            idleDown:  { from: 9,  to: 9,  loop: true, speed: 1 },
            walkDown:  { from: 8,  to: 11,  loop: true, speed: 8 },

            idleLeft:  { from: 13, to: 13, loop: true, speed: 1 },
            walkLeft:  { from: 12,  to: 15, loop: true, speed: 8 },
        },
    }
);

export const rangerCharacterSprite = k.loadSprite("rangerCharacter", "/RetroRealm/assets/sprites/ranger.png",
    {
        sliceX: 4,
        sliceY: 4,
        anims: {
            idleUp:    { from: 1,  to: 1,  loop: true, speed: 1 },
            walkUp:    { from: 0,  to: 3,  loop: true, speed: 8 },

            idleRight: { from: 5,  to: 5,  loop: true, speed: 1 },
            walkRight: { from: 4,  to: 7,  loop: true, speed: 8 },

            idleDown:  { from: 9,  to: 9,  loop: true, speed: 1 },
            walkDown:  { from: 8,  to: 11,  loop: true, speed: 8 },

            idleLeft:  { from: 13, to: 13, loop: true, speed: 1 },
            walkLeft:  { from: 12,  to: 15, loop: true, speed: 8 },
        },
    }
);

export const warriorCharacterSprite = k.loadSprite("warriorCharacter", "/RetroRealm/assets/sprites/warrior.png",
    {
        sliceX: 4,
        sliceY: 4,
        anims: {
            idleUp:    { from: 1,  to: 1,  loop: true, speed: 1 },
            walkUp:    { from: 0,  to: 3,  loop: true, speed: 8 },

            idleRight: { from: 5,  to: 5,  loop: true, speed: 1 },
            walkRight: { from: 4,  to: 7,  loop: true, speed: 8 },

            idleDown:  { from: 9,  to: 9,  loop: true, speed: 1 },
            walkDown:  { from: 8,  to: 11,  loop: true, speed: 8 },

            idleLeft:  { from: 13, to: 13, loop: true, speed: 1 },
            walkLeft:  { from: 12,  to: 15, loop: true, speed: 8 },
        },
    }
);