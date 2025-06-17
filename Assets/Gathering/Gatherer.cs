using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherer : MonoBehaviour
{
    public float interactionRange = 2f;
    public int baseDamage = 1;
    public int axeDamage = 2;

    [Header("Sound Effects")]
    public AudioClip treeChopSound;
    public AudioClip rockMineSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    hit.collider.GetComponent<Tree>()?.ChopTree();
                    if (treeChopSound != null)
                    {
                        audioSource.PlayOneShot(treeChopSound);
                    }
                }
                else if (hit.collider.CompareTag("Rock"))
                {
                    hit.collider.GetComponent<Rock>()?.MineRock();
                    if (rockMineSound != null)
                    {
                        audioSource.PlayOneShot(rockMineSound);
                    }
                }
            }
        }
    }
}