using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    // Player movement values
    public float speed;
    public float jumpVelocity;

    // Variables for jumping
    public bool isGrounded;
    bool canJump;
    private const float GRAVITY = -9.8f;
    [SerializeField] float gravityMultiplier;
    private Vector3 verticalVelocity; // current vertical velocity of player
    [SerializeField] float jumpOffset; // for smooth jumping
    public float groundDistance; // distance from the ground which "counts" as ground
    public Transform feet; // position of the player's feet
    public LayerMask groundMask; // layer for "ground" gameobjects

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        Jumping();
        Movement();
    }

    private void Movement()
    {
        // Moving left and right
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveVector = (transform.right * x) + (transform.forward * z);

        controller.Move(moveVector * speed * Time.deltaTime);
    }

    /// <summary>
    /// Jumping Logic
    /// canJump is slightly earlier (more range) than isGrounded for easy jumps
    /// </summary>
    private void Jumping()
    {
        canJump = Physics.CheckSphere(feet.position, jumpOffset, groundMask);
        isGrounded = Physics.CheckSphere(feet.position, groundDistance, groundMask);

        // Jump
        if (Input.GetButtonDown("Jump") && canJump)
        {
            verticalVelocity.y = jumpVelocity; // set initial velocity (pos)
        }

        // In the air, enable gravity
        if (!isGrounded) verticalVelocity.y += GRAVITY * gravityMultiplier * Time.deltaTime;

        controller.Move(verticalVelocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(feet.position, groundDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feet.position, jumpOffset);
    }
}
