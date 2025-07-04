using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject player;

    private int health = 100; 

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
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
