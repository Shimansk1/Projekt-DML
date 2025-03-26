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
            MyData = SaveGameManageris.CurrentSaveData.playerData;
            transform.position = MyData.PlayerPosition;
            transform.rotation = MyData.PlayerRotation;
            MyData.CurrentHealth = playerHealth.currentHealth;
            Debug.Log("Loaded");
            SaveGameManageris.LoadGame();
            
            Invoke(nameof(EnableMovement), 0.25f);
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