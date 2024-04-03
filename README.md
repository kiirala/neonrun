# neonrun
Endless runner meets danmaku - Unity learning project

Created as an entry for Games Job Fair Spring 2024 LITE - Unity Engine Programming Challenge

## Core ideas

* Endless runner with five lanes
* Top-down or slighly tilted view
* Neon light and space theming
* Tiny hitboxes: player sprite and an obstacle can overlap significantly before it's counted as a collision
* Obstacles come in pre-defined patterns with progressively increasing difficulty. The order of patterns is randomized
* Game speed constantly increases until a collision happens
* Graze: almost hitting an obstacle yields extra points and/or extra bomb power
* Bomb: limited amount of bombs, activating one clears all lanes from obstacles for a short time
  * Possible extra: emergency bomb - when bombing shortly after a collision, revives player but uses two bomb slots (or last remaining one)
* Focus: slows down game time, but yields less points and/or bomb power

## Implementation plan

To be done:

1. Graze visuals: visual effect when player receives extra points from grazing an obstacle
1. Focus visuals: visual effect when player is in focus mode
1. Bomb visuals: visual effect for activating a bomb
1. Bomb counter: give pre-defined number of bombs at start, decrease by one when a bomb is used, don't allow bombing when zero

Already completed:

1. Draw lanes on screen
1. Draw player sprite on screen
1. Control scheme v1: player can jump between lanes
1. Single obstacle appears at top of screen on a lane and moves to bottom, then repeats
1. Obstacle can collide with player - game stops
1. Multiple obstacles are spawned in a single pre-defined pattern repeatedly
1. Score counter - increases with time, stops on collision
1. Bomb mechanic / control scheme v2: player can trigger a bomb whenever, clears all obstacles on board
1. Graze mechanic: give extra points when player sprite touches an obstacle
1. Focus mechanic / control scheme v3: player can enter and exit focus mode. When in focus mode, game time runs slower
1. Start screen: wait for player input before game time starts and obstacles appear
1. End screen: after collision, when game is stopped, player input resets game state and enters active game mode
1. Short gameplay instructions in start screen
1. Retry prompt in end screen
