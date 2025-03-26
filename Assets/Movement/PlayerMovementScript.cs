using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;

    public float Speed = 12f;
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;

    public Transform GroundCheck;
    public Transform WaterCheck;
    public float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    public LayerMask WaterLayer; 

    private Vector3 velocity;
    private bool IsGrounded;
    private bool IsSwimming; 

    void Update()
    {
        if (Keyboard.current.leftShiftKey.isPressed && !IsSwimming && IsGrounded && Keyboard.current.wKey.isPressed)
        {
            Speed = 16f;
            //Debug.Log("is sprinting");
        }
        else
        {
            Speed = 12f;
            //Debug.Log("isnt sprinting");
        }

        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        IsSwimming = Physics.CheckSphere(WaterCheck.position, GroundDistance, WaterLayer);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (IsSwimming)
        {
            velocity.y = 0f; 
            controller.Move(move * (Speed / 2) * Time.deltaTime); 
        }
        else
        {
            if (IsGrounded && velocity.y < 0)
            {
                velocity.y = -2f; 
            }

            velocity.y += Gravity * Time.deltaTime;

            controller.Move(move * Speed * Time.deltaTime);
        }

        if (!IsSwimming && Input.GetButtonDown("Jump") && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
