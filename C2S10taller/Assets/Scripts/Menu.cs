using UnityEngine;
using System.Collections;  
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
public void Inicio()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

   public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
