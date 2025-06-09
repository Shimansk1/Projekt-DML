using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun Settings")]
    public Light sun;
    public float dayDurationInMinutes = 10f;
    public Gradient lightColorOverDay;
    public AnimationCurve lightIntensityOverDay;

    [Header("Sun Model (Visual)")]
    public Transform sunModel; // Fyzický model slunce

    [Header("Day Settings")]
    [Range(0f, 1f)] public float sunriseTime = 0.25f;  // 6:00
    [Range(0f, 1f)] public float sunsetTime = 0.833f;  // 20:00

    [Header("Time")]
    [Range(0, 1)] public float debugTimeOverride = -1f;
    public float currentTimeOfDay = 0f; // 0–1 (0 = 00:00, 1 = 24:00)

    [HideInInspector] public bool BlockLightControl = false;

    public bool IsNight => currentTimeOfDay < sunriseTime || currentTimeOfDay > sunsetTime;

    private void Update()
    {
        float timeMultiplier = 1f / (dayDurationInMinutes * 60f);
        currentTimeOfDay += Time.deltaTime * timeMultiplier;
        if (currentTimeOfDay >= 1f) currentTimeOfDay = 0f;

        if (debugTimeOverride >= 0f)
            currentTimeOfDay = debugTimeOverride;

        UpdateSun();
    }

    private void UpdateSun()
    {
        float rotation;

        if (currentTimeOfDay >= sunriseTime && currentTimeOfDay <= sunsetTime)
        {
            float t = Mathf.InverseLerp(sunriseTime, sunsetTime, currentTimeOfDay);
            rotation = Mathf.Lerp(0f, 180f, t);
        }
        else
        {
            float t;
            if (currentTimeOfDay < sunriseTime)
            {
                t = Mathf.InverseLerp(sunsetTime, 1f + sunriseTime, currentTimeOfDay + 1f);
            }
            else
            {
                t = Mathf.InverseLerp(sunsetTime, 1f + sunriseTime, currentTimeOfDay);
            }

            rotation = Mathf.Lerp(180f, 360f, t);
        }

        Quaternion sunRotation = Quaternion.Euler(rotation, 0f, 0f);

        // Správně otočenej model slunce (žlutý disk)
        if (sunModel != null)
            sunModel.rotation = sunRotation * Quaternion.Euler(180f, 0f, 0f);

        // Directional Light
        if (sun != null)
            sun.transform.rotation = sunRotation;

        // 🔒 Zabráníme přepisování barvy a intenzity při počasí
        if (!BlockLightControl && sun != null)
        {
            sun.color = lightColorOverDay.Evaluate(currentTimeOfDay);
            sun.intensity = lightIntensityOverDay.Evaluate(currentTimeOfDay);
        }
    }

    public float GetTime01() => currentTimeOfDay;

    public float GetTimeInHours() => currentTimeOfDay * 24f;
}
