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

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);


        anim.SetFloat("eje x", Mathf.Abs(moveInput));

        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;


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
            anim.SetBool("floor", true); // vuelve al suelo
        }
    }
}
