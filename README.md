
## **Adding Gunfire When Game starts**

learned what a GameObject is
learned what a prefab is and how to use prefabs
learned difference between start() and update()

update runs with fps
start runs once when the class is called for each object


## **Added Monster spawn and Auto Attack**

Worked on implementing a spawner system and learned how to move objects to specific positions dynamically. Gained a better understanding of Unity’s lifecycle methods, particularly how Start() runs once on initialization while Update() runs every frame. Discovered that prefab assets cannot store live GameObject data, only references and components, which clarified how to manage instantiation and object behavior at runtime.


## **Added Colliders**

Added 2D colliders

Bullets: Collider2D (trigger enabled)

Monsters: Collider2D (regular or trigger, with Rigidbody2D Kinematic)

Rigidbody2D setup

At least one object in the collision pair must have a Rigidbody2D for Unity to detect collisions.

Bullets: Kinematic Rigidbody2D, moves manually.

Monsters: Kinematic Rigidbody2D, follows path manually without physics interference.

Collision detection

Used OnTriggerEnter2D(Collider2D other) in the monster script.

Checked for a Bullet component in the colliding object.

Damage handling

Each bullet carries its own damage value.

On collision, monster’s health is reduced by the bullet’s damage, and the bullet is destroyed.

Homing / targeting behavior

Bullets can target a specific monster at spawn.

If the monster is destroyed before the bullet reaches it, the bullet continues in its last known direction.

Key lessons

2D and 3D collisions use different callbacks; using the wrong one prevents detection.

Kinematic Rigidbody2D allows movement while still detecting collisions without gravity interference.

Assigning a target per bullet avoids the need for a central monster list.


## **Code Refactoring & HP Text**

Refactored projectiles into a centralized structure:

Projectiles base class with Initialize(ProjectileType type)

Stats stored in ProjectileConfig dictionary

Prefab initialization via Projectiles.InitializePrefab()

Refactored monsters into a parallel architecture:

Monsters base class holding common stats (health, speed, count, rate)

MonsterConfig stores per-type stats (Square, Circle, Boss)

MonsterFactory handles spawning, tracking active monsters, and providing closest monster queries

SquareMonster inherits from Monsters and handles movement, damage, and destruction events

Added Boss prefab using the same SquareMonster script for testing purposes

GameEngine now initializes both projectiles and monsters using their respective factories/configs, removing hard-coded stats and spawn parameters from the scene

Added floating HP display above monsters using TextMeshPro 3D text, with offset and camera-facing rotation

HP display updates dynamically based on monster health

Offset is standardized per monster prefab

What was learned:

Centralizing configs allows adding new projectile or monster types without touching multiple scripts

Factories decouple spawning logic from scene/game logic

Proper prefab setup (scale, pivot, offsets) is critical for consistent UI (HP text) across different monster shapes


## **Added ShooterDeath when monster hit**




## **Restructured Entire Project**

some key concepts that i remember:
player made as prefab and spawned from GameLogicEngine
New GameStateEngine made to keep track of all prjectile, mosnter and player stats
TakeDamage made as a reusable script and given to player and monster
made physics layer for monster, projectilea and player
this allows to restrict colliion only to monster <-> player
and monster<-> bullet
projectile movement and monster movement made as reusable components
Centralised all of the existing models and classes


## **Added round spawning**

rather than having endless monsters spawn, added a round system inside game engine
after each round currently the monster stats are upgraded slightly
added hp display for player and monsters
added a menu scene before game scene to start game


## **Added Textures and Upgrades**

added publically available prefabs for projectiles
added upgrade mechanism to upgrade the player/projectiles upon rarity
and i dont rememebr what all i did cos i have been making changed continuosly for past 2 days and never committed the changes :)

