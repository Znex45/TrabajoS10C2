using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float tiempoVida = 0.5f; 

    private void Start()
    {
        // Se destruye automáticamente después de segundos
        Destroy(gameObject, tiempoVida);
    }
}
