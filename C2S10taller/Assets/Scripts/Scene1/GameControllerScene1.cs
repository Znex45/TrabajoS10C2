using UnityEngine;

public class GameControllerSecene1 : MonoBehaviour
{
    public Timer tiempoEscena;
    private bool done;
    void Start()
    {

    }

    void Update()
    {
    }


    public void TimeScene1()
    {
        if (done || tiempoEscena == null || GameManager.Instance == null) return;

        tiempoEscena.TimerStop();
        GameManager.Instance.SumaTimeGlobal(tiempoEscena.StopTime1);
        Debug.Log("Tiempo Escena 1 " + GameManager.Instance.Globaltime1);

        done = true;
    }
    private void OnDisable() => TimeScene1();
    private void OnDestroy() => TimeScene1();
}
