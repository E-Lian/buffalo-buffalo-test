Scene:
- When game starts, enemies will spawn at each spawn point.
Pressing space bar can reset everything and respawn the enemy (so like closing and rerunning the game, but it's faster)

- Each time enemies are spawned, the randomly selected daytime and enemy info are logged onto the console.

- Spawn points are represented as purple circles on ground.
The spawn points are arranged as follows:

1   3
2   4
  ↑
camera

- All enemies are represented as cubes with their types' corresponding color.



Scripts:
- EnemyType.cs: Scriptable Object for storing Enemy type data. 
The original EnemyTypes serve as templates and are not used directly. 
The system keeps a copy of each type at runtime (I call them runtime types), so that when game needs to respawn them, the previous edited stats won't be preserved.

- Enemy.cs: MonoBehaviour Object for the enemy cube
Basically is used to update the in-game object's material to the right color.

- SpawnPoint.cs: MonoBehaviour Object for spawn points
Each spawn point has a list of types that are allowed to be spawned at this point.
Handles all the logic of spawning (choose type using spawn rate and instantiating them)

- Logic.cs: MonoBehaviour Object for game logic
Handles all the other logic (day cycle, input handling, respawn...)
Clears all Enemy objects in scene (if any). Clones EnemyType at start (and each time space is pressed). Get random time and change runtime types accordingly. Tell all spawn points to spawn.
The whole process repeats when player wants to respawn enemies.