using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoInteractivo : MonoBehaviour
{
    //Variables de textos
    public GameObject dialogCanvas; // Referencia al objeto de Canvas que contiene el componente de texto
    public Text dialogText; // Referencia al componente de texto en el Canvas
    private bool isInRange = false;

    private void Start()
    {
        dialogCanvas.SetActive(false); // Ocultamos el Canvas al iniciar el juego
    }

    //Segun la colision activa el dialogo.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowDialog();
        }
    }

    private void ShowDialog()
    {
        dialogCanvas.SetActive(true); // Activamos el Canvas
        dialogText.text = ""; // Actualizamos el texto del Canvas
    }
}
