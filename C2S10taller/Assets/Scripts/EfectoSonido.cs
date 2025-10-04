using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EfectoSonido : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip colectar1;
    [SerializeField] private AudioClip colectar2;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(colectar2);
            Destroy(gameObject);
        }
    }
}
