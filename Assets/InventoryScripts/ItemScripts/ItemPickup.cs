using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviour
{
    public float PickupRadius = 1f;
    public InventoryItemData ItemData;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickupRadius;
    }

        
    private void OnTriggerEnter(Collider other)
    {
    //    if (Keyboard.current.fKey.isPressed)
        {
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

        if (!inventory) return;

        if (inventory.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
            Debug.Log("item picked up");
        }
        }
    }
    
}
