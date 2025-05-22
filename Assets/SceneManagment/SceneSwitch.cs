using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public string SceneToSwitch;

    // Pøidáš svùj defaultní skybox tady
    [SerializeField] private Material defaultSkybox;

    private void Start()
    {
        // Obnovíme skybox po spuštìní scény
        ApplySkybox();
    }

    public void ChangeScene()
    {
        // Použijeme coroutine, protože musíme poèkat na naètení scény
        StartCoroutine(SwitchSceneWithSkybox());
    }

    IEnumerator SwitchSceneWithSkybox()
    {
        // Naèti scénu
        SceneManager.LoadScene(SceneToSwitch);

        // Poèkej frame aby se scéna naèetla
        yield return null;

        // Znovu aplikuj skybox
        ApplySkybox();
    }

    void ApplySkybox()
    {
        if (defaultSkybox != null)
        {
            RenderSettings.skybox = defaultSkybox;
            DynamicGI.UpdateEnvironment(); // Obnova ambient svìtla
        }
        else
        {
            Debug.LogWarning("Skybox material není pøiøazen!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
