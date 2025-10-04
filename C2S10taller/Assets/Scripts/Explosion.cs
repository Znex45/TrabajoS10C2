using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float tiempoVida = 0.5f; // tiempo antes de destruir

    private void Start()
    {
        // Se destruye autom�ticamente despu�s de "tiempoVida" segundos
        Destroy(gameObject, tiempoVida);
    }
}
