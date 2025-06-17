using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BoatInteraction : MonoBehaviour
{
    public float sailAwaySpeed = 5f;
    public float endDelay = 8f;

    private bool isSailing = false;
    private bool hasStartedEndgame = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSailing)
        {
            isSailing = true;

            if (!hasStartedEndgame)
            {
                hasStartedEndgame = true;
                Invoke(nameof(EndGame), endDelay);
            }
        }
    }

    void Update()
    {
        if (isSailing)
        {
            transform.Translate(Vector3.forward * sailAwaySpeed * Time.deltaTime);
        }
    }

    void EndGame()
    {
        Debug.Log("🏁 KONEC HRY – odplul jsi do dálav.");
        SceneManager.LoadScene("Credits");

    }
}
