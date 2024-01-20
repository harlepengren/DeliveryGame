using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float acceleration = 20.0f;
    [SerializeField] float maxVelocity = 20.0f;
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] float drift = 1.2f;

    private Rigidbody rb;
    private PlayerInput controls;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        controls = new PlayerInput();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
      Vector2 move_direction = controls.Player.Move.ReadValue<Vector2>();

        // Forward and Backward Movement
        if(rb.velocity.magnitude < maxVelocity){
            rb.AddForce(transform.forward * move_direction.y * acceleration);
        }

        // Turning
        if(Mathf.Abs(move_direction.x) > 0.2 && rb.velocity.magnitude > 1)
        {
            Vector3 rotation_amount = new Vector3(0.0f,move_direction.x*rotationSpeed * rb.velocity.magnitude/maxVelocity * drift,0.0f);
            transform.Rotate(rotation_amount);
        }

    }

}
