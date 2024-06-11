using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    // Player movement values
    public float speed;
    public float jumpVelocity;

    // Variables for jumping
    bool isGrounded;
    private const float GRAVITY = -9.8f;
    private Vector3 verticalVelocity; // current vertical velocity of player
    public float groundDistance; // distance from the ground which "counts" as ground
    public Transform groundCheck; // position of the player's feet
    public LayerMask groundMask; // layer for "ground" gameobjects

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        #region Jumping

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            verticalVelocity.y = jumpVelocity; // set initial velocity (pos)
        }

        // In the air, enable gravity
        if (!isGrounded) 
        {
            verticalVelocity.y += GRAVITY * Time.deltaTime;
        }

        controller.Move(verticalVelocity * Time.deltaTime);

        #endregion

        #region Moving left and right

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveVector = (transform.right * x) + (transform.forward * z);
        
        controller.Move(moveVector * speed * Time.deltaTime);


        #endregion

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
