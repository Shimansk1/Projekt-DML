using UnityEngine;

public class TemperatureSystem : MonoBehaviour
{
    public DayNightCycle dayNightCycle;
    public WeatherManager weatherManager;
    public PlayerHealth playerHealth;
    public PlayerMovementScript playerMovement;

    public float checkInterval = 1f;
    public float currentTemperature;
    public float safeMinTemperature = 5f;
    public float safeMaxTemperature = 35f;
    public int damagePerTick = 5;

    public float fireBonus = 15f;
    public float clothingBonus = 10f;

    public bool nearFire = false;

    public float baseChangeStep = 1f;        // Normální krok
    public float waterChangeMultiplier = 2f; // Zrychlení ve vodě

    private float targetTemperature = 20f;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateTargetTemperature), checkInterval, checkInterval);
        InvokeRepeating(nameof(UpdateCurrentTemperature), checkInterval, checkInterval);
    }

    private void UpdateTargetTemperature()
    {
        float baseTemp = 20f;

        if (weatherManager != null)
        {
            switch (weatherManager.GetCurrentWeather())
            {
                case WeatherType.Sunny:
                    baseTemp += 10f;
                    break;
                case WeatherType.Rainy:
                    baseTemp -= 5f;
                    break;
                case WeatherType.Foggy:
                    baseTemp -= 2f;
                    break;
            }
        }

        if (dayNightCycle != null && dayNightCycle.IsNight)
        {
            baseTemp -= 8f;
        }

        if (nearFire)
        {
            baseTemp += fireBonus;
        }

        baseTemp += clothingBonus;

        // 🌊 Limitace ohřívání ve vodě
        if (playerMovement != null && playerMovement.IsSwimming)
        {
            baseTemp = Mathf.Min(baseTemp, 25f);
        }

        targetTemperature = baseTemp;
    }

    private void UpdateCurrentTemperature()
    {
        if (Mathf.Approximately(currentTemperature, targetTemperature)) return;

        float step = baseChangeStep;

        // 🌊 Pokud je hráč ve vodě, zrychlíme změnu teploty
        if (playerMovement != null && playerMovement.IsSwimming)
        {
            step *= waterChangeMultiplier;
        }

        if (currentTemperature < targetTemperature)
        {
            currentTemperature += step;
            if (currentTemperature > targetTemperature)
                currentTemperature = targetTemperature;
        }
        else if (currentTemperature > targetTemperature)
        {
            currentTemperature -= step;
            if (currentTemperature < targetTemperature)
                currentTemperature = targetTemperature;
        }

        //Debug.Log($"🌡 Aktuální teplota: {currentTemperature}°C");

        if (currentTemperature < safeMinTemperature || currentTemperature > safeMaxTemperature)
        {
            playerHealth.TakeDamage(damagePerTick);
            //Debug.Log("🔥 Extrémní teplota – hráč dostává damage!");
        }
    }
}