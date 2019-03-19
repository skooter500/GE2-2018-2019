# Game Engines 2 2018-2019

[![Video](http://img.youtube.com/vi/NR-zxfT0fTk/0.jpg)](http://www.youtube.com/watch?NR-zxfT0fTk)

## Resources
- [Class Facebook page](https://www.facebook.com/groups/2228012700814097/)
- [Assignment](ca.md)
- [A build of Infinite Forms](https://drive.google.com/file/d/1w24BcMAi6P1XmPc9D9ss6Lkro4KBvsMS/view?usp=sharing)
- [All about Infinite Forms](http://bryanduggan.org/forms)
- [A spotify playlist of music to listen to while coding](https://open.spotify.com/user/1155805407/playlist/5NYFsIFTgNOI93hONLbqNI)

## Contact me
* Email: bryan.duggan@dit.ie
* Twitter: [@skooter500](http://twitter.com/skooter500)
* [My website & other ways to contact me](http://bryanduggan.org)

## Last years course
- [2017-2018](https://github.com/skooter500/GE2-2017-2018)
	
## Assessment Schedule	
- Week 8 - CA proposal & Git repo - 10%
- Week 9 - Lab Test - 20%
- Week 12 - CA Submission & Demo - 40%

## Week 8 - Finite State Machines
- [Using ScriptableObjects & the delegate pattern toi make pluggable AI](https://unity3d.com/learn/tutorials/topics/navigation/finite-state-ai-delegate-pattern)

## Week 7 - Obstacle Avoidance

## Week 6 - VR steering wheel controller

## Week 5 - More wandering behaviours

### Lab
### Learning Outcomes
- Know how the harmoinc steerin behaviour works
- Program a procedural creature animation

### Part 1

- Add code to the harmonic steering behaviour to implement a vertical harmonic motion. 
- Use the spine animator with the harmonic steering behaviour to program this procedural creature animation:

[![YouTube](http://img.youtube.com/vi/B6sp6D-9O5o/0.jpg)](https://www.youtube.com/watch?v=B6sp6D-9O5o)

### Part 2

Work on your assignment

## Week 4 - Wandering behaviours

Previous lab tests:

- https://github.com/skooter500/GE2-LabTest2-2017

- https://github.com/skooter500/GE2-2017-Lab-Test

- https://github.com/skooter500/GE2-Labtest1-2018

Wandering behaviour examples:

[![YouTube](http://img.youtube.com/vi/qmtxqCU1X5Q/0.jpg)](https://www.youtube.com/watch?v=qmtxqCU1X5Q)

[![YouTube](http://img.youtube.com/vi/GW2K20ZhRW8/0.jpg)](https://www.youtube.com/watch?v=GW2K20ZhRW8)

[![YouTube](http://img.youtube.com/vi/ntDDkmhF5NY/0.jpg)](https://www.youtube.com/watch?v=ntDDkmhF5NY)

- JitterWander
- Harmonic
- Noise Wander

# Lab

Clone the repository for the course and create a seperate branch for your work today

## Part 1
Create a new scene and make this predator prey simulation. The prey will follow a path until the predator comes into range. When the predator is is range the prey will attack the predator by shooting at it. It only shoots at the predator if it is inside the field of view. The predator will get close to the prey, but will flee from the prey if the prey attacks it. You can use colliders and then disable and enable certain behaviours to implement the simulation.

[![YouTube](http://img.youtube.com/vi/SqThPN_ogJE/0.jpg)](https://www.youtube.com/watch?v=SqThPN_ogJE)

## Part 2

Do some work on your assignment!

## Week 3 - Pursue, Offset pursue & formations.

## Lab 3
### Learning Outcomes 
- Get some art assets to use in your assignment
- Refactor the code we have been writing to use components

### Lab
- Learn how to refactor a big class that does many things into lots of smaller classes that do one thing each

### Task 1
- Find some 3D models & art assets for your assignment & get them loaded into a scene in Unity
- Set up a git repo for your assignment

### Task 2

Out Boid class is getting big so now it's time to refactor the code so that each behaviour is a seperate MonoBehaviour.

Clone/pull the repo for the course

- Make an abstract base class called SteeringBehaviour that extends MonoBehavior
- Add one abstract method ```public abstract Vector3 Calculate()```
- Add a public field for *weight* of type float and also add a field for the Boid (the owner of the behaviour)
- Take each of the behaviours we wrote (seek, arrive, flee, path following, PlayerSteering) and make each of them extend SteeringBehaviour. Do the calculation of the force in Calculate
- Remove the boolean flags for the behaviours. They are not needed any more. Instead each boid will use whatever behaviours are attached to it.
- Give the Boid a field of type ```SteeringBehaviour[]```
- In the setup method of the Boid class, call GetComponents to get all the attached SteeringBehaviours
- Assign the boid field of each attached behaviour
- In the Boid Update method, iterate over the activated behaviours and sum the generated forces * the weights
- This will become more useful when we combine the behaviours together

## Week 2 - Flee, Path Following & PlayerSteering

## Learning Outcomes
- Learn how to use gizmos
- Know how to program a path following steering behaviour
- Know how to program a flee behaviour

### Part 1 - Seek & Arrive

- Clone/pull this repository to get the project we worked on in the class last week. 
- Create a branch for your work today called lab1
- Open the scene called Seek and Arrive. 
- In the Boid class, add a method called ```public void OnDrawGizmos()```. This method will get called by the Unity editor to draw "gizmos" in the scene view. These are things like lines, spheres and meshes that are useful for illustrating what is going on in your code. [Read more about the Gizmo API](https://docs.unity3d.com/ScriptReference/Gizmos.html)
- Gizmos only show up in the scene view in Unity and drawing gizmos will not impact on the performance of your game after you export it from Unity.
- Draw a sphere gizmo at the target position
- Draw a line gizmo to show the force. A good way to do this is to draw a Gizmo from the Boids position in the direction of the force vector. You might have to scale the force vecor to make the Gizmo visible.
- When it works, you should be able to see that the Seek force vector is always directed towards the target, while the arrive force is towards the target outside the slowing circle and then away from the target inside the slowing circle.

### Part 2 - Flee

- Implement a behaviour called ```Flee``` that causes the Boid to flee from a target when the target comes in range. To do this, add a method called ```Flee``` and a bool flag called ```FleeEnabled``` on the Boid class. You will also have to update the ```CalculateForces``` method. Set up your scene with a target game object and move the target around the scene and make sure the Flee force gets activated when the target comes in range.

### Part 3 - Path following

Check out this video:

[![Video](http://img.youtube.com/vi/eAfpnWI5jEI/0.jpg)](http://www.youtube.com/watch?v=eAfpnWI5jEI)

The scene contains a game object object with a script called ```Path``` attached. The path script implements the method ```OnDrawGizmos``` to draw lines between the waypoints on the path and to draw a sphere at each of the waypoints. The blue box is following the path. Notice that it turns to face each of the waypoints smoothly. Also it will bank to the left and right as it turns. Today you can try and program this behaviour. 

- Implement the ```Path``` script that contains a ```public List<Vector3> waypoints``` containing the waypoints. The 0th waypoint should be the position of the path gameobject itself and the positions of the attached children should be used to set the subsequent waypoints. You can populate this list in ```Awake```. 
- Add a bool flag on the ```Path``` class called ```IsLooped``` to indicate whether the path should loop back to the 0th waypoint when the Boid reaches the last waypoint. If this flag is set to be false, then don't draw a line gizmo back to the 0th waypoint.
- In the scene, create a path using the ```Path``` script you made.
- Add a a public field to the Boid class for the Path and drag the Path you made onto this in the Unity editor. 
- Now write code for a behaviour in Boid called ```FollowPath``` to get the Boid to move from waypoint to waypoint on the path using Seek and Arrive. If the ```IsLooped``` flag is true on the path, then the Boid should Seek the 0th waypoint again when it reaches the last waypoint. If this flag is set to be false, then the Boid should Arrive at the last waypoint. You will need to add a method called FollowPath and a boolean flag to indicate that the FollowPath behaviour is enabled. 

## Week 1 - Introduction to the course. Introduction to steering behaviours
- [Craig Reynolds original paper](https://www.red3d.com/cwr/papers/1999/gdc99steer.pdf)
- [Steering behaviours in Java](https://www.red3d.com/cwr/steer/)
- We implemented seek arrive and flee steering behaviours in the class. Check out scene4.unity

