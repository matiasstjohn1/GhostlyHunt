using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenuUI; // Referencia al objeto Canvas que contiene el menú de pausa.

    private bool isPaused = false;

    void Start()
    {
        // Asegurarse de que el menú de pausa no esté visible al inicio del juego.
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        //Si se presiona la tecla "P" o "Espacio" para pausar o reanudar el juego.
        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Si el juego está pausado, reanudar el juego.
            }
            else
            {
                PauseGame(); // Si el juego no está pausado, pausar el juego.
            }
        }
    }

    public void PauseGame()
    {
        //Activar el menú de pausa y pausar el juego.
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Establecer el tiempo a 0 para detener el juego.
        isPaused = true;
    }

    public void ResumeGame()
    {
        //Desactivar el menú de pausa y reanudar el juego.
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Establecer el tiempo de vuelta a 1 para reanudar el juego.
        isPaused = false;
    }

    // Método para el botón "Continuar" en el menú de pausa.
    public void OnContinueButton()
    {
        ResumeGame();
    }

    // Método para el botón "Salir" en el menú de pausa.
    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
