using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo; // punto desde donde sale la bala
    [SerializeField] private GameObject balaPrefab;
    [SerializeField] private float velocidadBala = 10f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    private void Disparar()
    {
        // Instanciamos la bala en la posici�n y con la rotaci�n del jugador
        GameObject nuevaBala = Instantiate(balaPrefab, controladorDisparo.position, transform.rotation);

        Rigidbody2D rb = nuevaBala.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Usamos la derecha del jugador (transform.right) como direcci�n real
            Vector2 direccion = transform.right;

            rb.linearVelocity = direccion * velocidadBala;

            // Aseguramos que la escala de la bala coincida con la direcci�n
            Vector3 escala = nuevaBala.transform.localScale;
            escala.x = Mathf.Sign(direccion.x) * Mathf.Abs(escala.x);
            nuevaBala.transform.localScale = escala;
        }

        Destroy(nuevaBala, 3f);
    }
}
