using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float daño = 1f;
    [SerializeField] private GameObject explosionPrefab; 

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * velocidad;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (other.CompareTag("Enemigo"))
        {

            EnemyBat enemigo = other.GetComponent<EnemyBat>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(daño);
            }
        }


        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
