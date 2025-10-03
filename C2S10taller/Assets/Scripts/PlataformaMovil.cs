using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMoimiento = 2f;

    private int siguientePlataforma = 0;
    private bool ordenPlataformas = true;

    private void Update()
    {
        if (puntosMovimiento.Length == 0) return;

        // Movimiento entre puntos
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[siguientePlataforma].position, velocidadMoimiento * Time.deltaTime);

        // Cambio de dirección al llegar al punto
        if (Vector2.Distance(transform.position, puntosMovimiento[siguientePlataforma].position) < 0.1f)
        {
            if (ordenPlataformas)
            {
                siguientePlataforma++;
                if (siguientePlataforma >= puntosMovimiento.Length)
                {
                    siguientePlataforma = puntosMovimiento.Length - 2;
                    ordenPlataformas = false;
                }
            }
            else
            {
                siguientePlataforma--;
                if (siguientePlataforma < 0)
                {
                    siguientePlataforma = 1;
                    ordenPlataformas = true;
                }
            }
        }
    }

    // ✅ CORREGIDO: ahora sí funcionan
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}

