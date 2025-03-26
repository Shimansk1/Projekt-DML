using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioClip menuMusic;
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Hudba z�stane hr�t i p�i p�echodu sc�n

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = menuMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        audioSource.Play();
    }
}
