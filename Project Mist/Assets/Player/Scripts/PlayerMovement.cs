using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement values")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float sprintMultiplier;
    [SerializeField] private bool isWalking = false;
    [SerializeField] private bool isSprinting = false;


    [Header("Jumping values")]
    public bool isGrounded = false;
    private bool canJump;
    private const float GRAVITY = -9.8f;
    [SerializeField] float gravityMultiplier;
    [SerializeField] private float jumpOffset; // for smooth jumping
    [SerializeField] private float groundDistance; // distance from the ground which "counts" as ground
    [SerializeField] private Transform feet; // position of the player's feet
    [SerializeField] private LayerMask groundMask; // layer for "ground" gameobjects

    private Vector3 verticalVelocity; // current vertical velocity of player

    public static PlayerMovement Instance { get; private set; }

    void Awake()
    {
        controller = this.GetComponent<CharacterController>();
        Instance = this;
    }

    void Update()
    {
        Vertical();
    }

    public void HandleMovement(Vector2 input, InputAction sprintAction)
    {
        if (input.magnitude <= 0)
        {
            isWalking = false;
            return;
        }
        else isWalking = true;

        float speedMultiplier = 1;
        if (sprintAction.ReadValue<float>() <= 0)
        {
            isSprinting = false;
        }
        else
        {
            isSprinting = true;
            speedMultiplier = sprintMultiplier;
        }

        float verticalSpeed = input.y * walkSpeed * speedMultiplier;
        float horizonalSpeed = input.x * walkSpeed * speedMultiplier;
        Vector3 moveVector = (transform.right * horizonalSpeed) + (transform.forward * verticalSpeed);
        controller.Move(moveVector * Time.deltaTime);
    }

    /// <summary>
    /// Jumping Logic
    /// canJump is slightly earlier (more range) than isGrounded for easy jumps
    /// </summary>
    public void HandleJump(InputAction jumpAction)
    {
        canJump = Physics.CheckSphere(feet.position, jumpOffset, groundMask);
        if (jumpAction.triggered && canJump)
        {
            verticalVelocity.y = jumpVelocity; // set initial velocity (pos)
        }
    }

    /// <summary>
    /// Updates vertical movement of player
    /// Gravity logic
    /// </summary>
    private void Vertical()
    {
        isGrounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);
        if (!isGrounded) verticalVelocity.y += GRAVITY * gravityMultiplier * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public bool GetIsWalking()
    {
        return isWalking && controller.velocity.magnitude > 0.05f;
    }

    public bool GetIsSprinting()
    {
        return isSprinting;
    }

    public float GetSprintMultiplier()
    {
        return sprintMultiplier;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feet.position, groundDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feet.position, jumpOffset);
    }
}
