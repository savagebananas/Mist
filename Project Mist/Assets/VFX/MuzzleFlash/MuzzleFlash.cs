using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] float intensity;
    [SerializeField] float intensityRandomization;
    [SerializeField] [Range(5f, 500f)] float range;
    [SerializeField] [Range(0.01f, 0.25f)] float duration;

    private void Awake()
    {
        light.range = range;
        light.intensity = intensity;
    }

    private void Start()
    {
        light.enabled = false;
    }

    public IEnumerator Flash()
    {
        light.intensity = Random.Range(intensity - intensityRandomization, intensity + intensityRandomization);
        light.enabled = true;
        yield return new WaitForSeconds(duration);
        light.enabled = false;
    }
}
