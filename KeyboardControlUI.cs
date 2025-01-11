using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class KeyboardControlUI : MonoBehaviour
{
    public Slider sliderX; // Reference to the slider for X Offset
    public Slider sliderY; // Reference to the slider for Y Offset
    public Slider sliderZ; // Reference to the slider for Z Offset
    public Toggle toggleX; // Reference to the toggle for Opposite X
    public Toggle toggleY; // Reference to the toggle for Opposite Y
    public Toggle toggleZ; // Reference to the toggle for Opposite Z
    public Canvas controlCanvas; // Reference to the Canvas to show/hide

    private int currentIndex = 0; // Tracks the currently selected UI element
    private Slider[] sliders; // Array of sliders for navigation
    private Toggle[] toggles; // Array of toggles for navigation
    private bool isOnToggle = false; // Flag to check if we are on a toggle

    private string filePath; // Path to save the settings

    void Start()
    {
        // Initialize slider and toggle arrays
        sliders = new Slider[] { sliderX, sliderY, sliderZ };
        toggles = new Toggle[] { toggleX, toggleY, toggleZ };

        // Set file path for saving
        filePath = Path.Combine(Application.persistentDataPath, "settings.json");

        // Ensure the canvas is visible at the start
        if (controlCanvas != null)
        {
            controlCanvas.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        // Toggle Canvas visibility with Tab key
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (controlCanvas != null)
            {
                controlCanvas.gameObject.SetActive(!controlCanvas.gameObject.activeSelf);
            }
        }

        // Save settings with S key
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSettings();
        }

        // Skip further UI navigation if canvas is hidden
        if (controlCanvas == null || !controlCanvas.gameObject.activeSelf)
        {
            return;
        }

        // Navigate between sliders and toggles
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % (sliders.Length + toggles.Length);
            isOnToggle = currentIndex >= sliders.Length;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + sliders.Length + toggles.Length) % (sliders.Length + toggles.Length);
            isOnToggle = currentIndex >= sliders.Length;
        }

        // Adjust slider value when on a slider
        if (!isOnToggle)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                sliders[currentIndex].value = Mathf.Clamp(sliders[currentIndex].value + 0.1f, sliders[currentIndex].minValue, sliders[currentIndex].maxValue);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sliders[currentIndex].value = Mathf.Clamp(sliders[currentIndex].value - 0.1f, sliders[currentIndex].minValue, sliders[currentIndex].maxValue);
            }
        }

        // Toggle state when on a toggle
        if (isOnToggle && Input.GetKeyDown(KeyCode.Space))
        {
            int toggleIndex = currentIndex - sliders.Length;
            toggles[toggleIndex].isOn = !toggles[toggleIndex].isOn;
        }

        // Highlight the selected slider or toggle
        HighlightSelection();
    }

    private void HighlightSelection()
    {
        // Highlight sliders
        for (int i = 0; i < sliders.Length; i++)
        {
            ColorBlock colorBlock = sliders[i].colors;
            if (i == currentIndex && !isOnToggle)
            {
                colorBlock.normalColor = Color.green; // Highlight selected slider
            }
            else
            {
                colorBlock.normalColor = Color.white; // Default color
            }
            sliders[i].colors = colorBlock;
        }

        // Highlight toggles
        for (int i = 0; i < toggles.Length; i++)
        {
            ColorBlock toggleColorBlock = toggles[i].colors;
            if (i == currentIndex - sliders.Length && isOnToggle)
            {
                toggleColorBlock.normalColor = Color.green; // Highlight selected toggle
            }
            else
            {
                toggleColorBlock.normalColor = Color.white; // Default color
            }
            toggles[i].colors = toggleColorBlock;
        }
    }

    private void SaveSettings()
    {
        SettingsData data = new SettingsData
        {
            sliderXValue = sliderX.value,
            sliderYValue = sliderY.value,
            sliderZValue = sliderZ.value,
            toggleXValue = toggleX.isOn,
            toggleYValue = toggleY.isOn,
            toggleZValue = toggleZ.isOn
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Settings saved to: " + filePath);
    }

    [System.Serializable]
    private class SettingsData
    {
        public float sliderXValue;
        public float sliderYValue;
        public float sliderZValue;
        public bool toggleXValue;
        public bool toggleYValue;
        public bool toggleZValue;
    }
}
