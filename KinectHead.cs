using UnityEngine;
using UnityEngine.UI; // For UI components
using com.rfilkov.kinect;

public class KinectHead : MonoBehaviour
{
    [Tooltip("Depth sensor index - 0 is the 1st one, 1 - the 2nd one, etc.")]
    public int sensorIndex = 0;

    [Tooltip("Index of the player tracked by this component. 0 means the 1st player, 1 - the 2nd one, etc.")]
    public int playerIndex = 0;

    [Tooltip("Offset to adjust the object's position relative to the head position.")]
    public Vector3 positionOffset = Vector3.zero;

    [Tooltip("Enable opposite movement for X, Y, Z axes.")]
    public bool oppositeX = false;
    public bool oppositeY = false;
    public bool oppositeZ = false;

    [Tooltip("UI-Text to display status messages.")]
    public UnityEngine.UI.Text statusText = null;

    private KinectManager kinectManager;
    private Vector3 initialPosition;

    void Start()
    {
        kinectManager = KinectManager.Instance;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (kinectManager && kinectManager.IsInitialized())
        {
            ulong userId = kinectManager.GetUserIdByIndex(playerIndex);

            if (kinectManager.IsUserTracked(userId) && kinectManager.IsJointTracked(userId, KinectInterop.JointType.Head))
            {
                // Get the head position from the Kinect
                Vector3 headPosition = kinectManager.GetJointPosition(userId, KinectInterop.JointType.Head);

                // Apply offset
                Vector3 adjustedPosition = headPosition + positionOffset;

                // Apply opposition logic
                if (oppositeX) adjustedPosition.x = headPosition.x * -1 + positionOffset.x;
                if (oppositeY) adjustedPosition.y = headPosition.y * -1 + positionOffset.y;
                if (oppositeZ) adjustedPosition.z = headPosition.z * -1 + positionOffset.z;

                // Update the game object's position
                transform.position = adjustedPosition;

                // Optional: Update the UI text with the head position
                if (statusText != null)
                {
                    statusText.text = $"Head Position: {headPosition}\nObject Position: {transform.position}\n" +
                                      $"Opposition: X({oppositeX}), Y({oppositeY}), Z({oppositeZ})";
                }
            }
            else
            {
                // If the head is not tracked, return to the initial position
                transform.position = initialPosition;

                if (statusText != null)
                {
                    statusText.text = "Head not tracked.";
                }
            }
        }
    }

    // Public methods to update settings dynamically
    public void UpdateOffsetX(float value)
    {
        positionOffset.x = value;
    }

    public void UpdateOffsetY(float value)
    {
        positionOffset.y = value;
    }

    public void UpdateOffsetZ(float value)
    {
        positionOffset.z = value;
    }

    public void ToggleOppositeX(bool value)
    {
        oppositeX = value;
    }

    public void ToggleOppositeY(bool value)
    {
        oppositeY = value;
    }

    public void ToggleOppositeZ(bool value)
    {
        oppositeZ = value;
    }
}
