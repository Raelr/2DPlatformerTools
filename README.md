# UntitledPlatformerProject

Authors: <b>Aryeh Zinn (Raelr)</b> and <b>William Anthony (WilliamAnth)</b>

This is an experimental project, intended to test the capabilities of implementing platformer physics within Unity. The intention is to develop a platformer without using certain inbuilt physics functions and components (like rigidbodies).

All collision detection is handled via a raycast system which, upon collision with an obstacle, reduces its velocity to an appropriate amount. The main components of the physics system (as of the current build) are:

> <b>Player:</b> Interprets all player inputs into movement.

> <b>Controller2D:</b> Takes an input velocity vector and ensures that all movements are handled properly. Also manages collision detection For the player (or agent using the system). 

> <b>RayCastUser:</b> An interface which is shared by all classes which need raycasts for collision detection (implemented by Controller2D).

<b>Credits:</b> Physics code was based on Sebastian Lague, who's youtube tutorial inspired this implementation. The link to his videos are below:

https://www.youtube.com/watch?v=MbWK8bCAU2w&list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz
