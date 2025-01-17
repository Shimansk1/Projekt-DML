using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;
    [SerializeField] private MouseLook mouseLook;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    private bool isInventoryOpen = false;

    protected override void Awake()
    {
        base.Awake();
        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }

    void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame && !isInventoryOpen)
        {
            isInventoryOpen = true;
            OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);

            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            mouseLook.canMove = false;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame && isInventoryOpen)
        {
            isInventoryOpen = false;

            Cursor.lockState = CursorLockMode.Locked; 
            Cursor.visible = false; 
            mouseLook.canMove = true; 
        }
    }


public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (primaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if (secondaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
