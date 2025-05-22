using UnityEngine;
using TransformSaveLoadSystem;

public class PlayerNeeds : MonoBehaviour
{
    public int maxHunger = 100;
    public int maxThirst = 100;

    public int currentHunger = 100;
    public int currentThirst = 100;

    public float hungerDecreaseInterval = 10f;
    public float thirstDecreaseInterval = 7f;

    private float hungerTimer;
    private float thirstTimer;
    private float lastDamageTime = 0f;
    private float damageCooldown = 1f;

    [SerializeField] private PlayerHealth playerHealth;

    void Start()
    {
        currentHunger = SaveGameManageris.CurrentSaveData.playerData.CurrentHunger;
        currentThirst = SaveGameManageris.CurrentSaveData.playerData.CurrentThirst;

        if (currentHunger <= 0) currentHunger = maxHunger;
        if (currentThirst <= 0) currentThirst = maxThirst;
    }

    void Update()
    {
        hungerTimer += Time.deltaTime;
        thirstTimer += Time.deltaTime;

        if (hungerTimer >= hungerDecreaseInterval)
        {
            ModifyHunger(-1);
            hungerTimer = 0f;
        }

        if (thirstTimer >= thirstDecreaseInterval)
        {
            ModifyThirst(-1);
            thirstTimer = 0f;
        }

        if ((currentHunger <= 0 || currentThirst <= 0) && Time.time - lastDamageTime > damageCooldown)
        {
            playerHealth.TakeDamage(1);
            lastDamageTime = Time.time;
        }

        SaveGameManageris.CurrentSaveData.playerData.CurrentHunger = currentHunger;
        SaveGameManageris.CurrentSaveData.playerData.CurrentThirst = currentThirst;
    }

    public void ModifyHunger(int amount)
    {
        currentHunger = Mathf.Clamp(currentHunger + amount, 0, maxHunger);
    }

    public void ModifyThirst(int amount)
    {
        currentThirst = Mathf.Clamp(currentThirst + amount, 0, maxThirst);
    }
}
