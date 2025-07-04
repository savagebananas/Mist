using UnityEngine;

public class FullAutoWeapon : MonoBehaviour, IEquippable
{
    [SerializeField] bool automaticFire = false;
    [SerializeField] float secondsPerShot = 0.1f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] int magazineSize = 30;
    [SerializeField] private MuzzleFlash muzzleFlash;


    private int ammo = 30;

    // Timers
    private float shootingTimer;
    private float reloadTimer;

    private bool mousePressed = false;
    private bool isShooting = false;
    private bool isReloading = false;

    void Update()
    {
        if (isReloading) Reload();
        if (!mousePressed) return;
        if (isShooting && !isReloading) Shoot();
    }

    private void Shoot()
    {
        if (shootingTimer <= 0)
        {
            if (ammo > 0)
            {
                ammo--;
                Debug.Log("Fire");

                // SFX
                // Particles
                muzzleFlash.Flash();

                shootingTimer = secondsPerShot;
            }
            else
            {
                reloadTimer = reloadTime;
                isReloading = true;
                isShooting = false;
            }
        }

        shootingTimer -= Time.deltaTime;
    }

    private void Reload()
    {
        reloadTimer -= Time.deltaTime;
        Debug.Log("Reloading");

        if (reloadTimer <= 0)
        {
            Debug.Log("Ready to fire");

            ammo = magazineSize;
            reloadTimer = reloadTime;
            isReloading = false;
            isShooting = true;
        }
    }

    public void UseItem()
    {
        mousePressed = true;
        isShooting = true;
    }

    public void StopUseItem()
    {
        mousePressed = false;
    }
}
