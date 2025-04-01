using TransformSaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSaveData : MonoBehaviour
{
    private PlayerData MyData = new PlayerData();
    private PlayerMovementScript MovementScript;
    [SerializeField]private PlayerHealth playerHealth;

    private void Awake()
    {
        MovementScript = GetComponent<PlayerMovementScript>();
    }
    void Update()
    {
        var transform1 = transform;
        MyData.PlayerPosition = transform1.position;
        MyData.PlayerRotation = transform1.rotation;
        MyData.CurrentHealth = playerHealth.currentHealth;

        if(Keyboard.current.f1Key.wasPressedThisFrame)
        {
            SaveGameManageris.CurrentSaveData.playerData = MyData;
            SaveGameManageris.SaveGame();
            Debug.Log("Saved");
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            MovementScript.enabled = false;

            SaveGameManageris.LoadGame(); // Tohle mus� b�t jako PRVN�

            MyData = SaveGameManageris.CurrentSaveData.playerData;
            transform.position = MyData.PlayerPosition;
            transform.rotation = MyData.PlayerRotation;
            playerHealth.currentHealth = MyData.CurrentHealth;

            Debug.Log("Loaded position: " + MyData.PlayerPosition);
            Invoke(nameof(EnableMovement), 0.25f);
        }
        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            SaveGameManageris.DeleteSaveData();
        }

    }
    private void EnableMovement()
    {
        MovementScript.enabled =true;
    }
}
[System.Serializable]
public struct PlayerData
{
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public int CurrentHealth;
}