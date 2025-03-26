using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveGameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject deathScreen;
    private PlayerHealth playerHealth;
    public bool respawned = false;
    [SerializeField] private MouseLook mouseLook;


    private void Update()
    {
        if(Keyboard.current.f1Key.wasPressedThisFrame)
        {
            SaveData();
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            TryLoadData();
        }
        if (Keyboard.current.f3Key.wasPressedThisFrame)
        {
            DeleteData();
        }
    }
    public static SaveData data;
    private void Awake()
    {
        data = new SaveData();
        SaveLoad.OnLoadGame += LoadData;
    }
    public void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }
    public static void SaveData()
    {
        var saveData = data;

        SaveLoad.Save(saveData);
    }
    public static void LoadData(SaveData _data)
    {
        data = _data;
    }
    public static void TryLoadData()
    {
        SaveLoad.Load();
    }
    public void Respawn()
    {
        Player.transform.position = new Vector3(-82.21f, -3.52f, 50.64f);
        deathScreen.SetActive(false);
        Debug.Log("Hr·Ë byl respawnut!");

        // Oprava - najdi PlayerHealth a resetuj ho
        playerHealth = Player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.currentHealth = 100;
            playerHealth.isDead = false;
            playerHealth.playerMovementScript.enabled = true;
        }
        else
        {
            Debug.LogError("PlayerHealth nebyl nalezen!");
        }

        respawned = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseLook.canMove = true;
    }
}
