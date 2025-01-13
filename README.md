# KinectAssociated

This repository contains Unity scripts designed for projects leveraging Azure Kinect for interaction, UI control, and object tracking. These scripts provide functionality for controlling UI elements, tracking user head position, slider management, and handling application focus.

## Features

1. **KeyboardControlUI.cs**  
   - Allows keyboard navigation to control UI sliders and toggles.  
   - Includes functionality to save UI states to a settings file.  

2. **KinectHead.cs**  
   - Tracks the user's head position using Azure Kinect and applies positional adjustments to objects in the Unity scene.  
   - Supports position offset and axis inversion for advanced control.  

3. **SliderController.cs**  
   - Handles slider value rounding and updates a text field with the current slider value.  

4. **WindowFocus.cs**  
   - Ensures the Unity application does not run in the background when it loses focus.  

## Setup Instructions

### Prerequisites

- Unity 2020.3 or later.
- Azure Kinect Sensor SDK.
- KinectManager asset for Unity (for handling Kinect data).

### Integration Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/hsuehyt/KinectAssociated.git
   ```
2. Open your Unity project and import the scripts into the desired folder.
3. Ensure that you have the Azure Kinect Sensor connected and configured using the KinectManager in your Unity scene.

### Additional Required Assets

1. Download and import the "AzureKinectExample" assets.
2. Only import the minimal required folders, including `KinectScripts`, `Resources`, and specific SDK folders such as `Kinect4AzureSDK`.

### Scene Configuration

1. **Setup Kinect Controller:**
   - Create an empty GameObject named `KinectController`.
   - Add the script `KinectManager` to this object.

2. **Setup Kinect Interface:**
   - Create another empty GameObject named `Kinect4Azure`.
   - Move `Kinect4Azure` under the `KinectController` as a child object.
   - Add the script `Kinect4AzureInterface` to the `Kinect4Azure` object.

3. Adjust settings for `KinectManager` and `Kinect4AzureInterface` as needed in the Inspector.

### Script Usage

#### KeyboardControlUI.cs
- Attach to a GameObject managing the UI.
- Link `Sliders`, `Toggles`, and the `Canvas` in the Unity Inspector.
- **Keyboard Controls**:
  - `Tab`: Show/hide the UI canvas.
  - `Arrow Keys`: Navigate between sliders and toggles.
  - `Space`: Toggle switches.
  - `S`: Save settings to a JSON file at the following location: C:/Users/user/AppData/LocalLow/CompanyName/ProductName/settings.json

#### KinectHead.cs
- Attach to any GameObject that needs to track the user's head position.
- Configure the `sensorIndex`, `playerIndex`, and `positionOffset` in the Inspector.
- Use public methods like `UpdateOffsetX`, `ToggleOppositeX` for runtime control.

#### SliderController.cs
- Attach to any slider that displays its value in a text field.
- Link the `Slider` and `Text` components in the Inspector.

#### WindowFocus.cs
- Attach to a GameObject in the Unity scene.
- Automatically pauses the application when the Unity window loses focus.