using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;             // Velocidad de movimiento
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator anim;               // Para controlar las animaciones
    private SpriteRenderer sr;           // Para voltear el sprite

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //(WASD o flechas)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Guardar dire
        moveInput = new Vector2(moveX, moveY).normalized;

        // Cambiar animacion 
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        // Girar 
        if (moveX > 0) sr.flipX = false;   // la derecha
        if (moveX < 0) sr.flipX = true;    //a la iuierda
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        rb.linearVelocity = moveInput * speed;
    }
}
