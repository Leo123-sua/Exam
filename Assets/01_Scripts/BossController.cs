using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;

    [Header("Entry State")]
    public float entrySpeed = 2.5f;

    [Header("Blink Attack")]
    public int blinkCount = 4;            // cuántas veces se teletransporta
    public float blinkInterval = 1.2f;    // cada cuánto tiempo
    public float teleportY = 3.5f;        // altura de los bordes superiores
    public float circularShotSpeed = 6f;

    void Start()
    {
        StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine()
    {
        // 1) Entry State
        yield return StartCoroutine(EntryState());

        // 2) Ciclo infinito SOLO Blink Attack
        while (true)
        {
            yield return StartCoroutine(BlinkAttack());
        }
    }

    // ----------------------------------------------------------
    // ENTRY STATE (baja desde arriba)
    // ----------------------------------------------------------
    IEnumerator EntryState()
    {
        Vector3 start = transform.position + new Vector3(0, 7f, 0);
        transform.position = start;

        Vector3 target = start + new Vector3(0, -4f, 0);

        while (Vector3.Distance(transform.position, target) > 0.2f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                entrySpeed * Time.deltaTime
            );
            yield return null;
        }

        yield return new WaitForSeconds(1f);
    }

    // ----------------------------------------------------------
    // BLINK ATTACK (teletransporte + disparo circular)
    // ----------------------------------------------------------
    IEnumerator BlinkAttack()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            // Teletransporte a un borde
            transform.position = RandomEdgePosition();

            yield return new WaitForSeconds(0.1f);

            // Disparo circular
            ShootCircular();

            yield return new WaitForSeconds(blinkInterval);
        }
    }

    Vector3 RandomEdgePosition()
    {
        int side = Random.Range(0, 3);

        float x = 0;
        float y = teleportY;

        if (side == 0)           x = Random.Range(-8f, 8f); // arriba
        else if (side == 1)      x = -8f;                   // izquierda
        else if (side == 2)      x = 8f;                    // derecha

        return new Vector3(x, y, 0);
    }

    void ShootCircular()
    {
        int shots = 10;
        float step = 360f / shots;

        for (int i = 0; i < shots; i++)
        {
            float angle = i * step;
            Vector3 dir = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad)
            );

            GameObject p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            p.GetComponent<Rigidbody2D>().velocity = dir * circularShotSpeed;
        }
    }
}
