using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;

    private int health = 100; 

    private void Awake()
    {
        instance = this;
    }

    public void TakeDamage(int i)
    {
        health -= i;
        // visual effects in UI
        // SFX

        if (health <= 0) Death();
    }

    public void Death()
    {

    }
}
