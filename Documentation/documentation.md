# Project Maze: Demo Project for Machine Learning in Unity

## The Unity Project

### Short Description
This project is a demo with the goal of making a simple Machine Learning algorithm that tries to find a treasure in a Maze.

### Elements
1. **Agent**: A simple cube, with the necessary properties.
2. **Maze**: A hand-built environment, consisting of a floor, cornering walls, and obstacles.
3. **Scenes**: There are two scenes, one called "Testbed", one called "Result".
    - The "Testbed" contains a lot of cloned environments for training the Agent.
    - The "Result" serves the purpose of showing off the Agent.

## Assets Folder Structure

The assets folder is organized to support the development and deployment of the machine learning agent. Below is the detailed structure of the assets folder:

### Folder Structure
- **Brains**
  - Contains ONNX files for different versions of the agent's brain, including final, without rotation, and with rotation versions.
- **ChestFree**
  - Houses the ChestFreeScene along with assets like FBX files for the chest (open and close states), coins, and related materials and textures.
- **Materials**
  - Stores material assets such as Blue, Gray, and Yellow materials.
- **ML-Agents**
  - Contains JSON files for different timers used in ML-Agents, segregated into a Timers subfolder.
- **Scenes**
  - Contains Unity scenes files for 'Result' and 'Testbed'.
- **Scripts**
  - Includes the main script `MoveToTargetAgent.cs`, a documentation markdown file, and a Tags folder with additional scripts like `Obstacle.cs`, `Target.cs`, and `Wall.cs`.

### Key Assets
- **MoveToTargetAgent.cs**
  - Core script for the agent's behavior in the maze.
- **labyrinthWall.prefab**
  - Prefab for the walls of the labyrinth.
- **ChestFreeScene.unity**
  - A Unity scene that is likely part of the ChestFree asset package.
- **Result.unity** and **Testbed.unity**
  - Scene files for the demonstration and testing environments.

### Miscellaneous Files
- **MoveToTarget.onnx**
  - The ONNX file at the root may be an earlier or alternative version of the agent's brain.
- **documentation.md**
  - A Markdown file presumably containing additional documentation.

This structure ensures a well-organized workflow, making each component of the project easily accessible for development and management purposes.

## Code Documentation: "MoveToTargetAgent.cs"

### Overview
The `MoveToTarget` class is a part of the Project Maze, which demonstrates the application of machine learning in Unity using the Unity ML-Agents toolkit. This class represents an agent designed to navigate through a labyrinth environment with the objective of finding a treasure.

### Class Variables

#### Serialized Fields
- `labyrinthWall`: Reference to the labyrinth wall prefab.
- `backgroundRenderer`: MeshRenderer for the background.
- `environment`: Reference to the environment GameObject.

#### Movement and Environment
- `episodeTimer`: Tracks the time elapsed in the current episode.
- `treasureLocation`: Stores the location of the treasure.
- `episodeDuration`: Duration of each episode in seconds.
- `movementSpeed`: Movement speed of the agent.
- `movementSpeedToTreasure`: Movement speed of the agent towards the treasure when detected.
- `lastPosition`: Last recorded position of the agent.
- `timeInCurrentArea`: Time spent in the current area.
- `timeThreshold`: Threshold time for staying in the same area.
- `areaRadius`: Radius considered as the same area.
- `idlePenalty`: Penalty for idling too long in the same area.
- `rewardCounter`: Counter for the number of times a reward is given.
- `maxRewardCount`: Maximum number of rewards before ending the episode.
- `rotateEnv`: Flag to rotate the environment.
- `treasureDetectedReward`: Reward given when the treasure is detected.

### Methods

#### Distance Calculation
- `CalculateDistanceToTreasure()`: Calculates the distance from the agent to the treasure.

#### Reward Handling
- `RewardAgentBasedOnDistance()`: Rewards the agent based on its distance from the treasure.

#### Treasure Detection
- `TreasureDetected()`: Checks if the treasure is detected by the agent's sensors.

#### Episode Management
- `OnEpisodeBegin()`: Initialization at the start of each episode.

#### Environment Manipulation
- `RotateEnvironmentRandomly()`: Rotates the environment randomly.

#### Agent Spawning
- `SpawnAgentRandomly()`: Determines a random spawn location for the agent.

#### Observations Collection
- `CollectObservations(VectorSensor sensor)`: Collects observations required for the agent's decision making.

#### Action Handling
- `OnActionReceived(ActionBuffers actions)`: Handles the received actions and updates the agent's state.

#### User Control
- `Heuristic(in ActionBuffers actionsOut)`: Allows user control for testing and debugging.

#### Collision Handling
- `OnTriggerEnter(Collider collision)`: Handles collisions with other objects.

### Usage
This agent is a key component of the Project Maze. Attach it to a GameObject in a Unity scene and configure the serialized fields in the Unity Inspector. The agent will interact with its environment according to the implemented logic, either in training or inference modes, showcasing machine learning capabilities in a Unity environment.
