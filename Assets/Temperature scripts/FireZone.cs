using UnityEngine;

public class FireZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TemperatureSystem temp = other.GetComponent<TemperatureSystem>();
            if (temp != null)
            {
                temp.nearFire = true;
                Debug.Log("🔥 Hráč se přiblížil k ohni.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TemperatureSystem temp = other.GetComponent<TemperatureSystem>();
            if (temp != null)
            {
                temp.nearFire = false;
                Debug.Log("🧊 Hráč odešel od ohně.");
            }
        }
    }
}
