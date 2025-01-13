using UnityEngine;
using UnityEngine.UI;
using com.rfilkov.kinect;

public class MultiKinectHeadTracking : MonoBehaviour
{
    [Tooltip("Index of the player tracked by this component. 0 means the 1st player, 1 - the 2nd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("Offset to adjust the object's position relative to the head position.")]
    public Vector3 positionOffset = Vector3.zero;

    [Tooltip("Enable opposite movement for X, Y, Z axes.")]
    public bool oppositeX = false;
    public bool oppositeY = false;
    public bool oppositeZ = false;

    [Tooltip("UI-Text to display status messages.")]
    public Text statusText = null;

    private KinectManager kinectManager;
    private Vector3 initialPosition;

    void Start()
    {
        kinectManager = KinectManager.Instance; // Ensure a single KinectManager is managing all devices
        initialPosition = transform.position;

        if (kinectManager == null)
        {
            Debug.LogError("KinectManager is not properly configured in the scene.");
        }
    }

    void Update()
    {
        if (kinectManager && kinectManager.IsInitialized())
        {
            ulong userId = kinectManager.GetUserIdByIndex(playerIndex);

            // Loop through all connected sensors
            for (int sensorIndex = 0; sensorIndex < kinectManager.GetSensorCount(); sensorIndex++)
            {
                KinectInterop.SensorData sensorData = kinectManager.GetSensorData(sensorIndex);

                if (sensorData != null && kinectManager.IsUserTracked(userId))
                {
                    // Check if the head joint is tracked for the current user
                    if (kinectManager.IsJointTracked(userId, KinectInterop.JointType.Head))
                    {
                        // Get the head position
                        Vector3 headPosition = kinectManager.GetJointPosition(userId, KinectInterop.JointType.Head);

                        // Apply position offset and opposition logic
                        Vector3 adjustedPosition = new Vector3(
                            oppositeX ? -headPosition.x : headPosition.x,
                            oppositeY ? -headPosition.y : headPosition.y,
                            oppositeZ ? -headPosition.z : headPosition.z
                        ) + positionOffset;

                        // Update the game object's position
                        transform.position = adjustedPosition;

                        // Update the UI text with the head position
                        if (statusText != null)
                        {
                            statusText.text = $"Sensor Index: {sensorIndex}\nHead Position: {headPosition}\nObject Position: {transform.position}\n" +
                                              $"Opposition: X({oppositeX}), Y({oppositeY}), Z({oppositeZ})";
                        }

                        return; // Exit the loop once tracking is successful
                    }
                }
            }

            // If no head is tracked, return to the initial position
            transform.position = initialPosition;
            if (statusText != null)
            {
                statusText.text = "Head not tracked by any Kinect sensor.";
            }
        }
    }
}
