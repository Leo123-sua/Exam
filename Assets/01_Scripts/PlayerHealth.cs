using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    int current;

    void Start()
    {
        current = maxHealth;
    }

    public void TakeDamage(int dmg)
    {
        current -= dmg;
        if (current <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Player murió");
        Destroy(gameObject);
    }
}
