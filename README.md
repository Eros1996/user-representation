# IMMERSE

IMMERSE is a Unity-based solution designed to enhance avatar tracking and motion control within virtual reality environments. This system seamlessly integrates hand tracking and motion controller input, providing a framework for a more immersive user experience. You can see a demo of the framework at https://youtu.be/fPVote4kz5E.

## Overview

IMMERSE is built to offer a flexible and extensible solution for incorporating diverse input modalities into virtual reality applications. It facilitates dynamic switching between hand tracking and motion controllers (with Quest family), real-time avatar calibration (with FinalIK), finger retargeting, animation control based on user input and animation recording.

## Usage

The initial step to utilize the framework involves configuring project settings based on the chosen technologies. The XR Plugin Management and an IK solver, at the moment FinalIK, packages must be downloaded and installed. Then, the IMMERSE framework must be downloaded using the package manager with the Add package by git URL button. After that, the user should also import the HandVisualizer Sample scene of the XR Hands, the Starter Asset and Hands Interaction Demo Samples of the XR Interaction Toolkit, and the sample scene of our package. All this can be done from the Unity3D package manager.
During development, various use cases were tested, and specific setup instructions for Interaction Profiles and OpenXR Feature Groups are provided based on the scenarios and technologies used:

1. **Quest 2:** Add the Oculus Touch Controller profile under the Interaction Profiles to use the controllers, or add the Hand Tracking Subsystem and the Meta Hand Tracking Aim under OpenXR Feature Groups to use hand tracking. Add all the above to be able to swap between the two InputMode. Use the Oculus OpenXR as Play Mode OpenXR Runtime.

2. **HTC Vive + Vive trackers:** Add the HTC Vive Controller Profile under the Interaction Profiles to use the controllers. Add the HTC Vive Tracker Profile under the Interaction Profiles to use the Vive trackers. Use SteamVR as Play Mode OpenXR Runtime.

3.  **Leap Motion controller:** First import the Ultraleap packages and install the Ultraleap Tracking package with version 6.13.0 following the instruction from their website https://docs.ultraleap.com/xr-and-tabletop/xr/unity/getting-started/index.html. Add the Hand Tracking Subsystem and the Meta Hand Tracking Aim under OpenXR Feature Groups. Under the Ultraleap tab in Project Setting enable the Update Meta Aim Input System. To align Leap Motion data and consider its different reference frame, one needs to enable Left and Right Arm IK_target-Leap offset Gameobjects.

A sample scene is provided in the package. When creating a new scene, the user should incorporate at least two GameObjects: the chosen character, properly configured as humanoid, and the XR Body Tracking Setup prefab. Then, one must attach the AvatarTrackingManager component to the character within the scene and configure the sitting or standing HMD offset based on the intended use. Customization options are available for each provided component. 
