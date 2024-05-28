# Reysics 🟣🟢
## Reysics is physics system for games only in C#🕹️
![dassa](https://github.com/MantiqStudio/Reysics/assets/167381007/1aae3c97-06f7-4b93-9c50-2fedf24c74fc)


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
## Reysics Body 👁️
To create a new physical body
```cs
ReysicsObject Object = new ReysicsObject();
```
To apply impulsion, movement and physical interactions
```cs
RRB = new ReysicsRigidBody(Object);
```
or use this for slow body:
```
RSB = new ReysicsSlowBody(Object);
```
## Checks 🏁
Point cast:
```cs
PointCast(Vector3 point, out ReysicsHit hit)
```
Box cast:
```cs
BoxCast(Vector3 point, Vector3 scale, Quaternion rotation, out ReysicsHit hit)
```
## Add Move & Force ➕
for RigidBody and SlowBody use this for add move:
```cs
AddMove(Vector3 move)
```
or 
```cs
Velocity += move;
```
for SlowBody use this for add slow move:
```cs
AddForce(Vector3 force)
```
![dsa](https://github.com/MantiqStudio/Reysics/assets/167381007/6cfa630a-9526-47f0-b9fa-d4669856e778)

# Basic Classes 🏛️
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
