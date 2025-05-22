using UnityEngine;

public class TempatureSystem : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public WeatherManager weatherManager;
    public PlayerHealth playerHealth;

    public float checkInterval = 5f;
    public float currentTemperature;
    public float safeMinTemperature = 5f;
    public float safeMaxTemperature = 35f;
    public int damagePerTick = 5;

    public float fireBonus = 15f;
    public float clothingBonus = 10f;

    public bool nearFire = false;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTemperature), checkInterval, checkInterval);
    }

    private void UpdateTemperature()
    {
        currentTemperature = 20f; // Základ

        if (weatherManager != null)
        {
            switch (weatherManager.GetCurrentWeather())
            {
                case WeatherType.Sunny:
                    currentTemperature += 10f;
                    break;
                case WeatherType.Rainy:
                    currentTemperature -= 5f;
                    break;
                case WeatherType.Foggy:
                    currentTemperature -= 2f;
                    break;
            }
        }

        if (dayNightCycle != null && dayNightCycle.IsNight)
        {
            currentTemperature -= 8f;
        }

        if (nearFire)
        {
            currentTemperature += fireBonus;
        }

        currentTemperature += clothingBonus;

        Debug.Log($"🌡 Aktuální teplota: {currentTemperature}°C");
        Debug.Log(dayNightCycle.currentTimeOfDay * 24);

        if (currentTemperature < safeMinTemperature || currentTemperature > safeMaxTemperature)
        {
            playerHealth.TakeDamage(damagePerTick);
            Debug.Log("🔥 Extrémní teplota – hráč dostává damage!");
        }
    }
}
