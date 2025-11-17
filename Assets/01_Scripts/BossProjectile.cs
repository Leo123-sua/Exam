using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 1;
    public float life = 4f;

    void Start()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
