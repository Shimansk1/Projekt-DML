using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour
{
    [System.Serializable]
    public class WeatherSceneSetup
    {
        public WeatherType weatherType;
        public GameObject[] skydomeObjects;
        public GameObject[] weatherEffects;
        public AudioClip weatherSound;
        [Range(0, 100)]
        public float probability;
        public Color directionalLightColor = Color.white;
    }

    public List<WeatherSceneSetup> weatherSetups;
    public float weatherChangeInterval = 300f;
    public Light directionalLight;

    // Přechodová mlha (slabší)
    public Color transitionFogColor = new Color(0.5f, 0.5f, 0.5f);
    public float transitionFogDensity = 0.01f;

    // Foggy počasí (hustší)
    public Color foggyColor = new Color(0.4f, 0.4f, 0.4f);
    public float foggyDensity = 0.035f;

    public float fogTransitionDuration = 2f;

    public WeatherSceneSetup currentWeather;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(ChangeWeatherRoutine());
    }

    private IEnumerator ChangeWeatherRoutine()
    {
        while (true)
        {
            var newWeather = GetWeatherByProbability();
            yield return StartCoroutine(TransitionWeather(newWeather));
            yield return new WaitForSeconds(weatherChangeInterval);
        }
    }

    private WeatherSceneSetup GetWeatherByProbability()
    {
        float roll = Random.Range(0f, 100f);
        float cumulative = 0f;

        foreach (var setup in weatherSetups)
        {
            cumulative += setup.probability;
            if (roll < cumulative)
                return setup;
        }

        return weatherSetups[0];
    }

    private IEnumerator TransitionWeather(WeatherSceneSetup newWeather)
    {
        // Přechodová mlha, ale ne při přechodu DO foggy
        if (newWeather.weatherType != WeatherType.Foggy)
        {
            yield return StartCoroutine(ApplyFog(true, transitionFogColor, transitionFogDensity));
        }

        yield return StartCoroutine(FadeOutAudio());

        if (directionalLight != null)
            yield return StartCoroutine(TransitionLightColor(newWeather.directionalLightColor));

        // Vypnout předchozí efekty
        if (currentWeather != null)
        {
            foreach (var obj in currentWeather.weatherEffects)
                if (obj) obj.SetActive(false);

            foreach (var sky in currentWeather.skydomeObjects)
                if (sky) sky.SetActive(false);
        }

        // Zapnout nové efekty
        foreach (var obj in newWeather.weatherEffects)
            if (obj) obj.SetActive(true);

        foreach (var sky in newWeather.skydomeObjects)
            if (sky) sky.SetActive(true);

        // Zvuk
        if (newWeather.weatherSound != null)
        {
            audioSource.clip = newWeather.weatherSound;
            audioSource.loop = true;
            audioSource.Play();
            yield return StartCoroutine(FadeInAudio());
        }

        currentWeather = newWeather;

        // Aplikuj finální mlhu
        if (newWeather.weatherType == WeatherType.Foggy)
        {
            yield return StartCoroutine(ApplyFog(true, foggyColor, foggyDensity));
        }
        else
        {
            yield return StartCoroutine(ApplyFog(false));
        }
    }

    private IEnumerator FadeOutAudio(float duration = 1f)
    {
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0f, t / duration);
                yield return null;
            }

            audioSource.volume = 0f;
            audioSource.Stop();
        }
    }

    private IEnumerator FadeInAudio(float duration = 1f)
    {
        float targetVolume = 1f;
        audioSource.volume = 0f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, targetVolume, t / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    private IEnumerator TransitionLightColor(Color targetColor, float duration = 1.5f)
    {
        if (directionalLight == null) yield break;

        Color startColor = directionalLight.color;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            directionalLight.color = Color.Lerp(startColor, targetColor, t / duration);
            yield return null;
        }

        directionalLight.color = targetColor;
    }

    private IEnumerator ApplyFog(bool enable, Color targetColor = default, float targetDensity = 0f)
    {
        RenderSettings.fog = true;

        Color startColor = RenderSettings.fogColor;
        float startDensity = RenderSettings.fogDensity;

        Color endColor = enable ? targetColor : startColor;
        float endDensity = enable ? targetDensity : 0f;

        for (float t = 0; t < fogTransitionDuration; t += Time.deltaTime)
        {
            RenderSettings.fogColor = Color.Lerp(startColor, endColor, t / fogTransitionDuration);
            RenderSettings.fogDensity = Mathf.Lerp(startDensity, endDensity, t / fogTransitionDuration);
            yield return null;
        }

        RenderSettings.fogColor = endColor;
        RenderSettings.fogDensity = endDensity;

        if (!enable)
            RenderSettings.fog = false;
    }

    public WeatherType GetCurrentWeather()
    {
        return currentWeather != null ? currentWeather.weatherType : WeatherType.Sunny;
    }
}