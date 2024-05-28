# Reysics 🟣🟢
## Reysics is physics system for games only in C#🕹️

# What is Reysics system 🔣
The Reysics physics system is a system that was designed specifically for games looking for lightness. Reysics does not connect to any specific game engine such as Unity, Godot, and Unreal because it has its own world in physics, and with simple connection features you can connect it to one of the engines.

# Getting started with Reysics ✅

## Why Reysics 🤷‍♂️
It is characterized by the fact that it can be connected to independent game engines and has a very strong lightness, which is the thing with which it competes

# First time 🥇
in the first use namespace reysics in your code
```cs
using reysics
```
## Reysics class
Reysics class is the manager of the physics world
- Gravity (Vector3) : The amount of force with which a body moves in meters per second
- Objects (List<ReysicsObject>) : All bodies entering the world

## ReysicsObject
It represents the physical body on which construction is being done, such as moving objects
- position (Vecotr3)
- rotation (Quaternion)
- scale (Vector3)

## ReysicsRigidBody
It is responsible for a body that reacts when interacting, is susceptible to impulsivity, and is affected by gravity
- Target (ReysicsObject) : The body to be applied to
- Velocity (Vecotr3) : The force to be moved in this second
- AngularVelocity (Vector3) : {SOON}
- GravityScale (float) : How much is affected by the gravity of the world

### ReysicsSlowBody
It is a system based on the ReysicsRigidBody system that is intended to move smoothly
- SlowVelcotiy (Vector3) : The amount of movement smoothly through the sampling unit
- Unit (float) : The amount of movement smoothly in one second

## ReysicsHit
It is a type of information when two objects collide or entangle
- Point (Vector3 array) : Sites of interference or collision
- Object (ReysicsObject) : Intervening or colliding body
