using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private GameObject player;
    public Camera cam;
    public float sensitivity;

    float xRotation;
    float yRotation;

    void Start()
    {
        player = this.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Look up and down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Look left and right
        yRotation += mouseX;

        player.transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
