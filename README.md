# UntitledPlatformerProject

Authors: <b>Aryeh Zinn (Raelr)</b> and <b>William Anthony (WilliamAnth)</b>

This is an experimental project, intended to test the capabilities of implementing platformer physics within Unity. The intention is to develop a platformer without using certain inbuilt physics functions and components (like rigidbodies). The project also features a full set of custom UI and level editing tools. The tools are located in a separate folder, while the physics section is located in its own, dedicated location. 

<b> Platformer Tools: </b>

All collision detection is handled via a raycast system which, upon collision with an obstacle, reduces its velocity to an appropriate amount. The main components of the physics system (as of the current build) are:

<b>Player:</b> Interprets all player inputs into movement.

<b>Controller2D:</b> Takes an input velocity vector and ensures that all movements are handled properly. Also manages collision detection For the player (or agent using the system). 

The controller has two delegates:

> <b>onCollision: </b>triggers as soon as a colision occurs. This is where you'd check for collisions with enemies, environemnt, etc. 

> <b>ignoreCollision: </b>these are the conditions which must be met for the player to 'ignore' a collision with a specific entity or type.

> <b>NOTE: </b>onCollision is called before ignoreCollision. This is important because if a condition is set for onCollision (i.e: the player dies when hitting an enemy), the onCollision delegate will call first (and would kill the player), and then the ignorecollision would be called. If you intend to have the player walk through certain objects, you should simply not set an onCollision delegate. 

<b>RayCastUser:</b> An interface which is shared by all classes which need raycasts for collision detection (implemented by Controller2D).

<b>Credits:</b> Physics code was based on Sebastian Lague, who's youtube tutorial inspired this implementation. The link to his videos are below:

https://www.youtube.com/watch?v=MbWK8bCAU2w&list=PLFt_AvWsXl0f0hqURlhyIoAabKPgRsqjz

<b>Level Editor Tools:</b>

The level editor is comprised of a worldgrid, a selection tile grid, brush buttons, and UI panels. This system does not utilise the canvas system to create its UI. The current build is comprised of two button types: SelectionIcons and selectionButtons:  

> <b>selectionIcon: </b>These icons are used for storing tile information for the level editor. They can only be selected by the standard brush tool.

> <b>selectionButton: </b>These are specific buttons which, when clicked, assign the player's tool to the button's corresponding tool. Thus far, only two tools exist: the standard brush and the eraser tool. 

