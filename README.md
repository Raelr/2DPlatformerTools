# UntitledPlatformerProject

Authors: Aryeh Zinn (Raelr) and William Anthony (WilliamAnth)

This is a basic Unity project featuring platformer mechanics within The Unity Engine. The intention is to develop a platformer without using certain functions within Unity such as rigidbodies or the inbuilt unity physics. It does utilise colliders for the sake of raycast hit detection, however all functions which handle movement and force additions were custom built.

All collision detection is handled via a raycast system which, upon collision with an obstacle, redues velocity to an appropriate amount. The main components of the physics system (as of the current build) are:

> Player: Interprets all player inputs into movement.
> Controller2D: Takes an input velocity vector, hadles collisions, and ensures that all movements are handled properly. 
> RayCastUser: An interface which is shared by all classes which need raycasts for collision detection (implemented by Controller2D).

