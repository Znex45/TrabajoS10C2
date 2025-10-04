using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EfectoSonido : MonoBehaviour
{
    
    [SerializeField] private AudioClip colectar1;
    
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControladorSonidos.Instance.PlaySound(colectar1);
            Destroy(gameObject);
        }
    }
}