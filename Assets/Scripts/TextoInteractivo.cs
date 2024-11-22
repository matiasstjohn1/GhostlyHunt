using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoInteractivo : MonoBehaviour
{
    //Variables de textos
    public GameObject dialogCanvas; // Referencia al objeto de Canvas que contiene el componente de texto
    public Text dialogText; // Referencia al componente de texto en el Canvas
    public string texto;

    private void Start()
    {
        dialogCanvas.SetActive(false); // Ocultamos el Canvas al iniciar el juego
    }

    //Segun la colision activa el dialogo.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowDialog();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogCanvas.SetActive(false);
        }
    }
    private void ShowDialog()
    {
        dialogCanvas.SetActive(true); // Activamos el Canvas
        dialogText.text = texto; // Actualizamos el texto del Canvas
    }
}
