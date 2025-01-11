using UnityEngine;

public class WindowFocus : MonoBehaviour
{
    void Update()
    {
        if (!Application.isFocused)
        {
            Debug.Log("Unity application lost focus. Bringing it back...");
            Application.runInBackground = false; // Ensures app doesn't run in the background
        }
    }
}
