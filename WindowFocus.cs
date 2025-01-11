using UnityEditor;

[InitializeOnLoad]
public static class PreventPauseOnLostFocus
{
    static PreventPauseOnLostFocus()
    {
        EditorApplication.playModeStateChanged += (state) =>
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                EditorApplication.isPaused = false; // Ensure it's not paused
            }
        };
    }
}
