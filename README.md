# User Representation

The User Representation System is a Unity-based solution designed to enhance avatar tracking and motion control within virtual reality environments. This system seamlessly integrates hand tracking and motion controller input, providing a framework for a more immersive user experience.

https://github.com/Eros1996/user-representation/assets/34738198/b3e5cbd8-44ee-4d23-8874-1694ac6993f5

## Overview

The User Representation System is built to offer a flexible and extensible solution for incorporating diverse input modalities into virtual reality applications. It facilitates dynamic switching between hand tracking and motion controllers (with Quest 2), real-time avatar calibration, finger retargeting, and animation control based on user input.

## System Components

### AvatarTrackingManager

Coordinates the initialization and setup of key components within the Avatar Tracking System.

- Dynamic creation of an offset for sitting or standing HMD configurations.
- Setup of XR events for mode switching.
- Calibration information setup for VRIK.
- Avatar component discovery and initialization.

### FingersRetargeting

Enables finger retargeting for hand tracking, ensuring a natural representation of hand movements in the avatar.

- Adjusts the scale of the avatar's hand based on XR hand joint data.
- Maps XR hand joint rotations to corresponding avatar finger rotations.

### OnButtonPress

Listens for button presses and releases, triggering associated UnityEvents.

- Listens for button press and release events from the input system.
- Invokes UnityEvents on button press and release.

### AnimateOnInput

Enables the animation of the avatar based on user input.

- Reads input values from the input system.
- Animates the avatar based on defined animation inputs.

### RecordingAnimation

- Record avatar movement in .anim and .csv formats.
- Decide which bones to record.

## Usage

1. **Initialization:** Attach the AvatarTrackingManager script to the main GameObject in the scene. Configure the sitting or standing HMD offset based on the intended use.

2. **Input Mode Switching:** Hand tracking is triggered when the XR input modality is set to TrackedHand. Motion controller input is triggered when the XR input modality is set to MotionController.

3. **Calibration:** The system performs real-time calibration for both hand tracking and motion controller modes. Calibration aligns the avatar's position and movements with the user's physical movements.

4. **Customization:** Customize finger retargeting settings in the FingersRetargeting script. Define animation inputs and associated UnityEvents in the AnimateOnInput script.
