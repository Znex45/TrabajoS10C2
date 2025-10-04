using UnityEngine;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;   // Opcional: "Monedita", "Copo", etc. (solo informativo)
    [SerializeField] private int itemValue; // Cuánto suma al puntaje (si <=0, se usa 1)

    private static int score;
    private static TMP_Text uiScore;

    private static void RefrescarUI()
    {
        if (!uiScore)
        {
            var go = GameObject.Find("TextPuntaje"); // Asegúrate que se llame exactamente así
            if (go) uiScore = go.GetComponent<TMP_Text>();
        }

        if (uiScore) uiScore.text = $"SCORE: {score}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int delta = (itemValue <= 0) ? 1 : itemValue;

        score += delta;
        RefrescarUI();

        Destroy(gameObject);
    }
}
