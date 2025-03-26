using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Hudba zùstane hrát i pøi pøechodu scén

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        audioSource.Play();
    }
}
