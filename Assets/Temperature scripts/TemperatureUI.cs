using UnityEngine;
using UnityEngine.UI;

public class TemperatureUI : MonoBehaviour
{
    public TemperatureSystem tempSystem;
    public DayNightCycle dayNightCycle;

    public Text temperatureText;
    public Text timeText;

    private void Update()
    {
        if (tempSystem != null && temperatureText != null)
        {
            float temp = tempSystem.currentTemperature;
            temperatureText.text = $"🌡 {temp:F1}°C";
        }

        if (dayNightCycle != null && timeText != null)
        {
            float hours = dayNightCycle.GetTimeInHours();
            int h = Mathf.FloorToInt(hours);
            int m = Mathf.FloorToInt((hours - h) * 60);
            timeText.text = $"🕒 {h:D2}:{m:D2}";
        }
    }
}
