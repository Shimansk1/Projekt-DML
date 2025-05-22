using TransformSaveLoadSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData MyData = new PlayerData();
    private PlayerMovementScript MovementScript;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerNeeds playerNeeds;

    private void Awake()
    {
        MovementScript = GetComponent<PlayerMovementScript>();
        playerNeeds = GetComponent<PlayerNeeds>();
    }

    void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            MyData.PlayerPosition = transform.position;
            MyData.PlayerRotation = transform.rotation;
            MyData.CurrentHealth = playerHealth.currentHealth;
            MyData.CurrentHunger = playerNeeds.currentHunger;
            MyData.CurrentThirst = playerNeeds.currentThirst;

            SaveGameManageris.CurrentSaveData.playerData = MyData;
            SaveGameManageris.SaveGame();
            Debug.Log("?? Saved");
        }

        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            MovementScript.enabled = false;

            SaveGameManageris.LoadGame();
            MyData = SaveGameManageris.CurrentSaveData.playerData;

            transform.position = MyData.PlayerPosition;
            transform.rotation = MyData.PlayerRotation;

            playerHealth.currentHealth = MyData.CurrentHealth;
            playerNeeds.currentHunger = MyData.CurrentHunger;
            playerNeeds.currentThirst = MyData.CurrentThirst;

            Debug.Log("?? Loaded position: " + MyData.PlayerPosition);
            Invoke(nameof(EnableMovement), 0.25f);
        }

        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            SaveGameManageris.DeleteSaveData();
            Debug.Log("? Save deleted");
        }
    }

    private void EnableMovement()
    {
        MovementScript.enabled = true;
    }
}

[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public int CurrentHealth;
    public int CurrentHunger;
    public int CurrentThirst;
}
