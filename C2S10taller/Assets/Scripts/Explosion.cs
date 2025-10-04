using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float tiempoVida = 0.5f; 

    private void Start()
    {
        // Se destruye autom�ticamente despu�s de segundos
        Destroy(gameObject, tiempoVida);
    }
}
