using System.Collections;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public int framesToReset = 10;
    [SerializeField] private float vida = 3f;
    [SerializeField] private GameObject explosionPrefab;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;
    private bool colisionplayer;
    private Animator animator;
    [SerializeField] private AudioClip Explosion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);

            if (movement.x != 0)
                sr.flipX = movement.x < 0;
        }
        else
        {
            movement = Vector2.zero;
        }

        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            colisionplayer = true;
            StartCoroutine(ResetCollisionFlag(framesToReset));
        }

        animator.SetBool("ColisionaPlayer", colisionplayer);
    }

    private IEnumerator ResetCollisionFlag(int frames)
    {
        for (int i = 0; i < frames; i++)
            yield return null;

        colisionplayer = false;
        animator.SetBool("ColisionaPlayer", colisionplayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // Método para recibir daño
    public void RecibirDaño(float cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    // Método cuando muere
    private void Muerte()
    {
        Debug.Log("¡Murió el murciélago!");

        // yamar explosión
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            ControladorSonidos.Instance.PlaySound(Explosion);
        }

        Destroy(gameObject); // eliminar enemigo
    }
}
