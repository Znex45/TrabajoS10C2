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
    private bool isDead = false;  
    private Vector3 startPosition;
    [SerializeField] private AudioClip Salto;
    [SerializeField] private AudioClip caida;
    [SerializeField] private AudioClip Muerte;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();


        startPosition = transform.position;
    }

    void Update()
    {

        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        anim.SetFloat("eje x", Mathf.Abs(moveInput));

        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        // Salto
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            anim.SetBool("floor", false);
            ControladorSonidos.Instance.PlaySound(Salto);
        }

        anim.SetBool("floor", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("floor", true);
            ControladorSonidos.Instance.PlaySound(caida);
        }

        if (collision.gameObject.CompareTag("Enemigo"))
        {
            StartCoroutine(Morir());
        }
    }

    private IEnumerator Morir()
    {
        isDead = true;  
        rb.linearVelocity = Vector2.zero; 
        anim.SetTrigger("death"); 


        yield return new WaitForSeconds(1f);


        transform.position = startPosition;

        isDead = false;
        anim.SetBool("floor", true);
        ControladorSonidos.Instance.PlaySound(Muerte);
    }
}
