using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public string SceneToSwitch;

    // P�id� sv�j defaultn� skybox tady
    [SerializeField] private Material defaultSkybox;

    private void Start()
    {
        // Obnov�me skybox po spu�t�n� sc�ny
        ApplySkybox();
    }

    public void ChangeScene()
    {
        // Pou�ijeme coroutine, proto�e mus�me po�kat na na�ten� sc�ny
        StartCoroutine(SwitchSceneWithSkybox());
    }

    IEnumerator SwitchSceneWithSkybox()
    {
        // Na�ti sc�nu
        SceneManager.LoadScene(SceneToSwitch);

        // Po�kej frame aby se sc�na na�etla
        yield return null;

        // Znovu aplikuj skybox
        ApplySkybox();
    }

    void ApplySkybox()
    {
        if (defaultSkybox != null)
        {
            RenderSettings.skybox = defaultSkybox;
            DynamicGI.UpdateEnvironment(); // Obnova ambient sv�tla
        }
        else
        {
            Debug.LogWarning("Skybox material nen� p�i�azen!");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
