using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f, jumpForce = 12f;
    public Transform GraundCheck;
    public LayerMask GroundLayer;

    [Header("Disparo")]
    public GameObject ProyectilePrefab;
    public float ProyectileSpeed = 10f;

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- Movimiento ---
        float h = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        if (h > 0) sr.flipX = false;
        else if (h < 0) sr.flipX = true;

        // --- Saltar ---
        bool grounded = Physics2D.OverlapCircle(GraundCheck.position, 0.12f, GroundLayer);
        if (Input.GetButtonDown("Jump") && grounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // --- Disparo ---
        if (Input.GetKeyDown(KeyCode.R))
            Shoot();
    }

    void Shoot()
    {
        if (ProyectilePrefab == null) return;

        Vector3 dir = sr.flipX ? Vector3.left : Vector3.right;
        Vector3 spawnPos = transform.position + dir * 0.6f;

        GameObject p = Instantiate(ProyectilePrefab, spawnPos, Quaternion.identity);

        // ENVIAMOS la dirección correcta al proyectil
        p.GetComponent<Proyectile>().Init(dir, 1, ProyectileSpeed);
    }
}
