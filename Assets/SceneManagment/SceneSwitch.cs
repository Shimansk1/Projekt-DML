using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitch : MonoBehaviour
{
    [SerializeField] public string SceneToSwitch;
    void Start()
    {

    }

    void Update()
    {

    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneToSwitch);        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}