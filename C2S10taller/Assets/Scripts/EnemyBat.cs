using System.Runtime.CompilerServices;
using System.Collections;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public int framesToReset = 10; // cantidad de frames a esperar antes de resetear

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr; // Anadido para controlar el flip
    private bool colisionplayer;
    private Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is creado
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); // Obtener el SpriteRenderer
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);

            // Cambiar la direccion del sprite segun el movimiento
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
            yield return null; // espera 1 frame por iteracion

        colisionplayer = false;
        animator.SetBool("ColisionaPlayer", colisionplayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}



