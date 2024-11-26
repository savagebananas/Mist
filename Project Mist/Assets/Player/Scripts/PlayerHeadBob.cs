using UnityEngine;

public class PlayerHeadBob : MonoBehaviour
{
    [SerializeField] private bool enable = true;
    [SerializeField, Range(0, 0.1f)] private float camYAmplitude = 0.015f;
    [SerializeField, Range(0, 0.1f)] private float camXAmplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float frequency = 10f;

    [SerializeField] Transform camera = null;
    [SerializeField] Transform cameraHolder = null;

    [SerializeField] private float toggleSpeed = 0; // minimum speed of player for headbob
    private Vector3 startPos;
    private CharacterController controller;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        startPos = camera.localPosition;
    }

    private void Update()
    {
        if (!enable) return;

        CheckMotion();
        ResetCamPos();

        camera.LookAt(FocusTarget());
    }

    private void HeadBob()
    {
        Vector3 difference = Vector3.zero;
        difference.y += Mathf.Sin(Time.time * frequency) * camYAmplitude;
        difference.x += Mathf.Cos(Time.time * frequency / 2) * camXAmplitude;

        camera.localPosition += difference;
    }

    /// <summary>
    /// Checks if player is moving on the ground
    /// </summary>
    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        if (speed < toggleSpeed) return;
        if (!playerMovement.isGrounded) return; // if in the air, don't headbob
        HeadBob();
    }

    private void ResetCamPos()
    {
        if (camera.localPosition == startPos) return;
        camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15f;
        return pos;
    }
}
