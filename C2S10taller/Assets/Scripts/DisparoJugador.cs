using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;
    [SerializeField] private float velocidadBala = 10f;
    [SerializeField] private AudioClip Disparo;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    private void Disparar()
    {

        GameObject nuevaBala = Instantiate(bala, controladorDisparo.position, Quaternion.identity);

        Rigidbody2D rb = nuevaBala.GetComponent<Rigidbody2D>();
        if (rb != null)
        {

            float direccion = transform.localScale.x;


            rb.linearVelocity = new Vector2(direccion * velocidadBala, 0f);


            Vector3 escala = nuevaBala.transform.localScale;
            escala.x = direccion * Mathf.Abs(escala.x);
            nuevaBala.transform.localScale = escala;
            ControladorSonidos.Instance.PlaySound(Disparo);
        }


        Destroy(nuevaBala, 3f);
    }
}
