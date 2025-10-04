using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public string nextSceneName = "Congratulations";
    [SerializeField] private bool logTimeOnTransition = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (logTimeOnTransition)
        {
            var timer = Object.FindFirstObjectByType<Timer>();
            if (timer != null)
            {
                // Stop and read time before changing scene
                timer.TimerStop();
                float t = timer.ObtenerTiempoFinal();

                int mm = (int)(t / 60f);
                int ss = (int)(t % 60f);
                int cc = Mathf.FloorToInt((t - (mm * 60 + ss)) * 100f);

                Debug.Log($"Tiempo total Snow -> {nextSceneName}: {mm:00}:{ss:00}:{cc:00}  ({t:F2} s)");
            }
            else
            {
                Debug.LogWarning("No se encontro un Timer en la escena para registrar el tiempo.");
            }
        }

        SceneManager.LoadScene(nextSceneName);
    }
}


