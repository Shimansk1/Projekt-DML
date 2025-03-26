using UnityEngine;
using UnityEngine.UI;
using TransformSaveLoadSystem;
using System.Collections;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private MouseLook mouseLook;
    public int maxHealth = 100;
    public int currentHealth;
    public float fallThreshold = 10f; 
    public float damageMultiplier = 10f;
    public Image damageEffect;
    public GameObject deathScreen;
    [SerializeField] public PlayerMovementScript playerMovementScript;
    private SaveGameManager saveGameManager;

    public bool isDead = false;
    private Vector3 lastGroundedPosition;

    void Start()
    {
        saveGameManager = FindObjectOfType<SaveGameManager>();
        if (saveGameManager == null) Debug.LogError("SaveGameManager není ve scénì!");

        // Naèítání hodnoty ze save souboru
        //currentHealth = SaveGameManageris.CurrentSaveData.playerData.CurrentHealth;
        currentHealth = 100;

        //Debug.Log("Naètené CurrentHealth ze save souboru: " + SaveGameManageris.CurrentSaveData.playerData.CurrentHealth);
        //Debug.Log("Hodnota currentHealth po naètení: " + currentHealth);

        lastGroundedPosition = transform.position;
    }


    
    void Update()
    {
        if (isDead) return;

        if (IsGrounded())
        {
            float fallDistance = lastGroundedPosition.y - transform.position.y;
            if (fallDistance > 3)
            {

                Debug.Log(fallDistance);
            }
            if (fallDistance > fallThreshold)
            {
                TakeDamage((int)((fallDistance - fallThreshold) * damageMultiplier));
            }
            lastGroundedPosition = transform.position;
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame) currentHealth = SaveGameManageris.CurrentSaveData.playerData.CurrentHealth;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 2.1f);
    }

    void TakeDamage(int damage)
    {
        Debug.Log("hit");
        currentHealth -= damage;
        //SaveGameManageris.CurrentSaveData.playerData.CurrentHealth = currentHealth;
        //SaveGameManageris.SaveGame();
        StartCoroutine(FadeDamageEffect());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FadeDamageEffect()
    {
        damageEffect.color = new Color(1f, 0f, 0f, 0.8f); 
        yield return new WaitForSeconds(0.5f);

        float fadeDuration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0.8f, 0f, elapsedTime / fadeDuration);
            damageEffect.color = new Color(1f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        damageEffect.color = new Color(1f, 0f, 0f, 0f); 
    }


    void Die()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseLook.canMove = false;
        isDead = true;
        deathScreen.SetActive(true);
        playerMovementScript.enabled = false;
        saveGameManager.respawned = false;
    }
}
