using UnityEngine;
using TMPro;

public class CongratulationsUI : MonoBehaviour
{
    [Header("Object names in scene")]
    [SerializeField] private string canvasScoreName = "CanvasScore";
    [SerializeField] private string txtTotalItemsName = "TextEL.RE";
    [SerializeField] private string txtEnemiesName = "Enemigos eliminados";
    [SerializeField] private string txtTimeName = "TextTiempototal";

    void Start()
    {
        // 1) Read and stop timer (even if HUD is inactive)
        float t = ReadFinalTimeSeconds();

        // 2) Hide HUD after reading time
        var hud = GameObject.Find(canvasScoreName);
        if (hud) hud.SetActive(false);

        // 3) Fill totals (keeping your labels)
        AppendValueToLabel(txtTotalItemsName, CollectibleItem.GetTotalItems().ToString());
        AppendValueToLabel(txtEnemiesName, EnemyBat.GetKilledCount().ToString());

        // 4) Format and fill total time (mm:ss:cc)
        int mm = (int)(t / 60f);
        int ss = (int)(t % 60f);
        int cc = Mathf.FloorToInt((t - (mm * 60 + ss)) * 100f);
        AppendValueToLabel(txtTimeName, $"{mm:00}:{ss:00}:{cc:00}");

        // 5) Console log
        Debug.Log($"Tiempo total final: {mm:00}:{ss:00}:{cc:00} ({t:F2} s)");
    }

    private float ReadFinalTimeSeconds()
    {
        // Find the Timer even if it is inactive or under DontDestroyOnLoad
        Timer timer = Object.FindFirstObjectByType<Timer>(FindObjectsInactive.Include);
        float t = 0f;

        if (timer != null)
        {
            timer.TimerStop();                 // stop if still running
            t = timer.ObtenerTiempoFinal();    // read final seconds
        }

        // Fallback to GameManager, if you store it there
        if (t <= 0.0001f && GameManager.Instance != null)
            t = GameManager.Instance.Globaltime1;

        return t;
    }

    private static void AppendValueToLabel(string goName, string value)
    {
        if (string.IsNullOrEmpty(goName)) return;
        var go = GameObject.Find(goName);
        if (!go) return;

        var tmp = go.GetComponent<TMP_Text>();
        if (!tmp) return;

        // Keep existing label and append value after colon
        string baseText = tmp.text;
        int cut = baseText.LastIndexOf(':');
        if (cut >= 0) baseText = baseText.Substring(0, cut + 1);

        tmp.text = $"{baseText} {value}";
    }
}


