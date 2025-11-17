using UnityEngine;

public class Proyectile : MonoBehaviour
{
    Rigidbody2D rb;
    int damage;
    float speed;

    public float LifeTime = 4f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector3 direction, int dmg, float spd)
    {
        damage = dmg;
        speed = spd;

        rb.velocity = direction * speed;

        Destroy(gameObject, LifeTime);
    }

    // --- Detectar colisiones ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
