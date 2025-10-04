using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- Movimiento lateral ---
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Animación de correr
        anim.SetFloat("eje x", Mathf.Abs(moveInput));

        // Voltear sprite
        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        // --- Salto ---
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("floor", false);  // al saltar deja de estar en el suelo
        }

        // Actualizamos el parámetro floor siempre
        anim.SetBool("floor", isGrounded);
    }

    // Detecta cuando toca el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // asegúrate que el suelo tenga el tag "Ground"
        {
            isGrounded = true;
            anim.SetBool("floor", true); // vuelve al suelo
        }
    }
}
