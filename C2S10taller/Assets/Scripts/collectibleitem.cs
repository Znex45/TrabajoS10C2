using UnityEngine;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private string itemName;     // "Monedita" o "Copo" (si lo dejas vacio usa el nombre del objeto)
    [SerializeField] private int itemValue = 1;   // Puntos que suma al score (si <=0 se usa 1)

    private static int monedas, copos, bananas, score;
    private static TMP_Text uiMonedas, uiCopos, uiBananas, uiScore;

    private const string LABEL_MONEDAS = "monedas recogidas: ";
    private const string LABEL_COPOS = "copos recogidos: ";
    private const string LABEL_BANANAS = "bananas recogidas: ";
    private const string LABEL_SCORE = "SCORE: ";

    private static void RefrescarUI()
    {
        // Lazy-binding de referencias UI por nombre en la jerarquia
        if (!uiMonedas)
        {
            var go = GameObject.Find("TextMonedas");
            if (go) uiMonedas = go.GetComponent<TMP_Text>();
        }
        if (!uiCopos)
        {
            var go = GameObject.Find("TextCopos");
            if (go) uiCopos = go.GetComponent<TMP_Text>();
        }
        if (!uiBananas)
        {
            var go = GameObject.Find("TextBananas");
            if (go) uiBananas = go.GetComponent<TMP_Text>();
        }
        if (!uiScore)
        {
            var go = GameObject.Find("TextPuntaje");
            if (go) uiScore = go.GetComponent<TMP_Text>();
        }

        if (uiMonedas) uiMonedas.text = LABEL_MONEDAS + monedas;
        if (uiCopos) uiCopos.text = LABEL_COPOS + copos;
        if (uiBananas) uiBananas.text = LABEL_BANANAS + bananas;
        if (uiScore) uiScore.text = LABEL_SCORE + score;
    }

    private void Start() => RefrescarUI(); // muestra 0 al inicio

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int puntos = (itemValue <= 0) ? 1 : itemValue;
        string n = string.IsNullOrWhiteSpace(itemName) ? gameObject.name : itemName;
        n = n.ToLower();

        // Conteos (1 por item recogido)
        if (n.Contains("moneda")) monedas += 1;
        else if (n.Contains("copo")) copos += 1;
        else if (n.Contains("banana") || n.Contains("banano") || n.Contains("platan")) bananas += 1;

        // Puntaje total
        score += puntos;

        RefrescarUI();
        Destroy(gameObject);
    }

    // Sumar puntos desde otros scripts (ej: EnemyBat al morir)
    public static void AddScore(int amount)
    {
        if (amount <= 0) return;
        score += amount;
        RefrescarUI();
    }

    // Getters para otras escenas (Congratulations)
    public static int GetMonedas() => monedas;
    public static int GetCopos() => copos;
    public static int GetBananas() => bananas;
    public static int GetScore() => score;
    public static int GetTotalItems() => monedas + copos + bananas;

    // Reset general (opcional)
    public static void ResetAll()
    {
        monedas = copos = bananas = score = 0;
        RefrescarUI();
    }
}



