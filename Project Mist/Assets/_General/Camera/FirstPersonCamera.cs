using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    Camera cam;

    public static FirstPersonCamera Instance { get; private set; }

    private void Awake()
    {
        cam = GetComponent<Camera>();
        Instance = this;
    }

    public Camera GetCam()
    {
        return cam;
    }
}
