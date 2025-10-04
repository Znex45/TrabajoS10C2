using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallDeath : MonoBehaviour
{
    [Header("Altura límite de caída")]
    [SerializeField] private float fallLimitY = -10f;

    [Header("Tiempo antes de reaparecer")]
    [SerializeField] private float respawnDelay = 0.5f;

    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead && transform.position.y < fallLimitY)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (anim != null)
        {
            anim.SetTrigger("Death");
        }

        Invoke("ReloadScene", respawnDelay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
