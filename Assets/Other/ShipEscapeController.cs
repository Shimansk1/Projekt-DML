using System.Collections;
using UnityEngine;
using TMPro;

public class ShipEscapeController : MonoBehaviour
{
    [Header("Legacy Text Panel")]
    public GameObject legacyPanel;

    [Header("Zvuk posádky")]
    public AudioSource crewAudioSource;
    public AudioClip crewShoutClip;

    [Header("Loď")]
    public Transform ship;
    public Transform escapeTarget;
    public float shipSpeed = 5f;

    [Header("Hráč")]
    public PlayerMovementScript player;

    private bool hasJumped = false;
    private bool shipDeparted = false;

    void Start()
    {
        if (legacyPanel != null)
        {
            legacyPanel.SetActive(true);
        }

        if (crewAudioSource != null && crewShoutClip != null)
        {
            crewAudioSource.clip = crewShoutClip;
            crewAudioSource.loop = true;
            crewAudioSource.Play();
        }
    }

    void Update()
    {
        if (!hasJumped && player != null && player.IsSwimming)
        {
            hasJumped = true;

            crewAudioSource.Stop();

            if (legacyPanel != null)
            {
                legacyPanel.SetActive(false);
            }
        }

        if (hasJumped && !shipDeparted && ship != null && escapeTarget != null)
        {
            ship.position = Vector3.MoveTowards(
                ship.position,
                escapeTarget.position,
                shipSpeed * Time.deltaTime
            );

            if (Vector3.Distance(ship.position, escapeTarget.position) < 0.5f)
            {
                ship.gameObject.SetActive(false); 
                shipDeparted = true;
            }
        }
    }
}
