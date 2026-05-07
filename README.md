# Clowning Around: An Augmented Reality Carnival Game

## Overview
"Clowning Around" is an Augmented Reality (AR) application developed for the Meta Quest 3. Built using the Unity 3D game engine, this project aims to bring the fun of simple, highly interactive carnival games directly into your home, lowering the barrier of expensive fairground prices. 

The game utilizes spatial awareness to spawn clown-resembling bowling pins on real-world elevated surfaces like tables and desks. Players use their VR controllers to shoot baseballs and knock down the pins before the clock runs out.

## Features
* **Dynamic Environment Mapping:** Utilizes the Meta Quest scene mesh feature to read your room and dynamically spawn targets on flat surfaces.
* **Fast-Paced Gameplay:** Players have 45 seconds to score as many points as possible. When hit, the pins animate a "wither away" effect.
* **Diegetic UI:** Inspired by Half-Life: Alyx, your current score and remaining time are conveniently displayed on a GUI attached to your left controller.
* **Local Leaderboard:** At the end of the 45-second round, players can enter their initials to save their high score locally on the headset.
* **Immersive Audio:** Features looping carnival-themed music to enhance the atmosphere.

## Controls
* **Right Controller Trigger:** Fire a baseball.
* **Right Controller 'B' Button:** Manually reset the clown pins and score.
* **Left Controller:** Lift your left hand to view the time and score display.

## Requirements
* **Hardware:** Meta Quest 3 Head-mounted Display (HMD).
* **Software:** Unity 2022.3.62f3.

## Build Instructions
To build and play the game on your own device, follow these steps:
1. Install Unity version 2022.3.62f3 via Unity Hub.
2. Clone or download this repository and open the project in Unity.
3. Connect your Oculus Quest 3 to your computer.
4. Ensure your Quest 3 is placed in Developer Mode (via the Meta Quest mobile app).
5. In Unity, go to File > Build Settings, ensure the platform is set to Android, and click Build and Run.
6. When the application launches on your headset, you will be prompted to create a detailed mesh of your environment. Make sure to scan the surfaces (like desks) that you want to interact with.
7. Play the game!

## Credits
**Developers:** Caleb Carpenter, Brad Chailland, Brice Stegall
