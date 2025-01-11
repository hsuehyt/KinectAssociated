using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;  // Reference to the Slider
    public Text valueText; // Reference to the Text displaying the value

    void Start()
    {
        // Initialize the text value
        UpdateSliderValue(slider.value);
    }

    public void UpdateSliderValue(float value)
    {
        // Round to the nearest 0.1
        float roundedValue = Mathf.Round(value * 10f) / 10f;

        // Update the slider value (optional, for consistent behavior)
        slider.value = roundedValue;

        // Update the Text with the rounded value
        valueText.text = roundedValue.ToString("0.0");
    }
}
