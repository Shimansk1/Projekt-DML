using SaveLoadSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSaveData : MonoBehaviour
{
    public int currentHealth = 10;
    private PlayerData MyData = new PlayerData();
    private PlayerMovementScript MovementScript;

    private void Awake()
    {
        MovementScript = GetComponent<PlayerMovementScript>();
    }
    void Update()
    {
        var transform1 = transform;
        MyData.PlayerPosition = transform1.position;
        MyData.PlayerRotation = transform1.rotation;
        MyData.CurrentHealth = currentHealth;

        if(Keyboard.current.f1Key.wasPressedThisFrame)
        {
            SaveGameManager.CurrentSaveData.playerData = MyData;
            SaveGameManager.SaveGame();
            Debug.Log("Saved");
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            MovementScript.enabled = false;
            SaveGameManager.LoadGame();
            MyData = SaveGameManager.CurrentSaveData.playerData;
            transform.position = MyData.PlayerPosition;
            transform.rotation = MyData.PlayerRotation;
            MyData.CurrentHealth = currentHealth;
            Debug.Log("Loaded");
            
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