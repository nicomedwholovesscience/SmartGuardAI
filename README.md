# SmartGuardAI

SmartGuardAI is a Unity 3D project focused on learning and implementing artificial intelligence for video games using C#.

The goal is to build an enemy AI system that can patrol, detect and chase the player, remember positions, and eventually investigate suspicious activity.

This project is being developed as a learning exercise and a portfolio project.

## Current Features

- **Enemy Patrol:** The enemy moves between configurable patrol points.
- **Patrol Waiting:** The enemy waits at each patrol point before continuing.
- **Player Detection:** The enemy detects the player within a configurable distance.
- **Chase Behavior:** The enemy follows the player when detected.
- **Finite State Machine:** Enemy behavior is organized into Patrol and Chase states.
- **Visual Feedback:** The enemy is green while patrolling and red while chasing.
- **Configurable Speeds:** Patrol and Chase use separate movement speeds.
- **Enemy Memory:** The enemy stores the player's last known position.

## Technologies

- Unity (3D)
- C#
- JetBrains Rider
- Git and GitHub

## How It Works

The enemy uses a Finite State Machine (FSM) to manage its behavior.

**Patrol State**
- Moves between assigned patrol points.
- Waits at each point.
- Switches to Chase when the player enters detection range.

**Chase State**
- Follows the player using distance-based detection.
- Moves faster than during patrol.
- Remembers the player's last detected position.
- Returns to Patrol when the player leaves detection range.

## Getting Started

1. Clone or download this repository.
2. Open the project using Unity Hub.
3. Open the main scene.
4. Check that the Enemy has its Player and Patrol Points references assigned.
5. Press Play to test the AI behavior.

## Planned Features

- Search state using the player's last known position.
- Field-of-view detection and obstacle checks.
- Navigation around obstacles using NavMesh.
- Additional enemy behaviors.
- Improved gameplay and user interface.

## Project Status

**In Development — Learning Project**

The project is being developed incrementally as I learn C# programming, Unity, and game AI architecture.

## Project Goals

- Improve my C# programming skills.
- Learn game AI algorithms and architecture.
- Practice writing maintainable and reusable code.
- Build a completed project for my game development portfolio.