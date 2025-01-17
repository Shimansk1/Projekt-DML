using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;

    private float xRotation = 0f;
    public bool canMove = true; // Nov� prom�nn� pro povolen� pohybu kamery

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!canMove) return; // Pokud nen� povolen pohyb, ukon��me Update

        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}
