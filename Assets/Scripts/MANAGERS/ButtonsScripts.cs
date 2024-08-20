using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScripts : MonoBehaviour
{
    //Boton de reinicio
    public void ButtonReset()
    {
        SceneManager.LoadScene(0);
    }
    //Boton de volver al menu
    public void ButtonMenu()
    {
        SceneManager.LoadScene(0);
    }
    //Boton del menu para jugar
    public void ButtonPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Boton de jugar directo.
    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }
    //Boton que cierra el juego
    public void ButtonExit()
    {
        Application.Quit();
    }

}
