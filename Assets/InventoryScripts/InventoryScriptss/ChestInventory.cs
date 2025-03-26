using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UniqueID))]
public class ChestInventory : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete {  get; set; }

    protected override void Awake()
    {
        base.Awake();
        SaveLoad.OnLoadGame += LoadInventory;
        //Debug.Log($"{gameObject.name} má IInteractable.");
    }
    private void Start()
    {
        var chestSaveData = new InventorySaveData(primaryInventorySystem, transform.position, transform.rotation); 

        SaveGameManager.data.chestDictionary.Add(GetComponent<UniqueID>().ID, chestSaveData);
    }
    protected override void LoadInventory(SaveData data)
    {
        if(data.chestDictionary.TryGetValue(GetComponent<UniqueID>().ID, out InventorySaveData chestData))
        {
            this.primaryInventorySystem = chestData.InvSystem;
            this.transform.position = chestData.Position;
            this.transform.rotation = chestData.Rotation;
        }
    }
    public void Interact(Interactor interactor, out bool interactSuccesful)
    {
        //Debug.Log("Interakce s truhlou probìhla!");
        OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem, 0);
        interactSuccesful = true;
        //if (interactSuccesful == true) Debug.Log("otevrena chesta");
        //Debug.Log("Truhla interagována, volám OnDynamicInventoryDisplayRequested.");

    }

    public void EndInteraction()
    {

    }

}
