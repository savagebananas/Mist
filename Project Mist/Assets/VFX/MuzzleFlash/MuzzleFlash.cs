using Unity.Cinemachine;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float intensity;
    [SerializeField] [Range(5f, 100f)] float range;
    [SerializeField] [Range(0.01f, 0.25f)] float duration;

    private void Awake()
    {
        light.range = range;
    }

    private void Start()
    {
        light.enabled = false;
    }

    public void Flash()
    {
        if (light == null) return;

        light.enabled = true;
        light.intensity = intensity;
        Invoke(nameof(HideLight), duration);
    }

    private void HideLight()
    {
        light.intensity = 0;
        light.enabled = false;
    }
}
