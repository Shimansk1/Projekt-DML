using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform InteractionPoint;
    public LayerMask InteractionLayer;
    public float InteractionPointRadius = 1f;

    public bool IsInteracting {  get; private set; }

    [SerializeField] private MouseLook mouseLook; // Odkaz na MouseLook script

    private void Update()
    {
        var colliders = Physics.OverlapSphere(InteractionPoint.position, InteractionPointRadius, InteractionLayer);

        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            for (int i = 0;i<colliders.Length;i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>(); 
                mouseLook.canMove = false; // Zastavit pohyb kamery
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (interactable != null) StartInteraction(interactable);
            }
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.visible = false;
            mouseLook.canMove = true; // Zastavit pohyb kamery
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccesful);
        IsInteracting = true;
    }

    void EndInteraction()
    {
        IsInteracting = false;
    }
}
