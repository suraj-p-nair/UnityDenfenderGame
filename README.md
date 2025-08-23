
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

