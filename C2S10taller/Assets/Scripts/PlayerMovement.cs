using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded = true;
    private bool isDead = false;   // para controlar si está muerto
    private Vector3 startPosition; // guardar la posición inicial

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // Guardamos la posición inicial
        startPosition = transform.position;
    }

    void Update()
    {
        // Si está muerto no puede moverse
        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Animación de correr
        anim.SetFloat("eje x", Mathf.Abs(moveInput));

        // Voltear sprite
        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("floor", false);
        }

        anim.SetBool("floor", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("floor", true);
        }

        // Si toca un enemigo → morir
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            StartCoroutine(Morir());
        }
    }

    private IEnumerator Morir()
    {
        isDead = true;  // bloquear movimiento
        rb.linearVelocity = Vector2.zero; // frenar
        anim.SetTrigger("death"); // activar animación de muerte

        // esperar el tiempo que dura la animación (ajústalo a lo que dure tu clip)
        yield return new WaitForSeconds(1f);

        // Reiniciar posición
        transform.position = startPosition;

        // Resetear estados
        isDead = false;
        anim.SetBool("floor", true);
    }
}
