using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleFPS : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 2f;
    public Transform playerCamera;
    public float sensitivity = 2f;

    private CharacterController characterController;
    private Vector3 verticalVelocity;
    private bool isJumping;

    private PlayerControls playerControls;
    private Vector2 moveInput;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        PlayerMovement();
        CameraMovement();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void PlayerMovement()
    {
        moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        float moveSpeed = playerControls.Player.Sprint.IsPressed() ? sprintSpeed : walkSpeed;
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            verticalVelocity.y = 0f;
            isJumping = false;
        }
        if (playerControls.Player.Jump.IsPressed() && !isJumping)
        {
            verticalVelocity.y += Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
            isJumping = true;
        }

        verticalVelocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void CameraMovement()
    {
        float mouseX = playerControls.Player.Look.ReadValue<Vector2>().x * sensitivity;
        float mouseY = playerControls.Player.Look.ReadValue<Vector2>().y * sensitivity;
        transform.Rotate(Vector3.up * mouseX);

        Vector3 currentRotation = playerCamera.rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - mouseY;
        if (desiredRotationX > 180)
        {
            desiredRotationX -= 360;
        }
        desiredRotationX = Mathf.Clamp(desiredRotationX, -90f, 90f);
        playerCamera.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}
