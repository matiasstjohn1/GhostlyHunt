using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour
{
    //Seccion inicio//
    public GameObject botonConfig; //Boton de creditos.
    public GameObject botonCredits; //Boton de configuracion.
    public GameObject back; //Boton para atras.

    public GameObject carpetaInicial;
    public GameObject carpetaConfig;
    public GameObject carpetaCredits;

    public void Config()
    {
        //True
        carpetaConfig.SetActive(true);
        //False
        carpetaCredits.SetActive(false);
        carpetaInicial.SetActive(false);
    }

    public void Credits()
    {
        //True
        carpetaCredits.SetActive(true);
        //False
        carpetaConfig.SetActive(false);
        carpetaInicial.SetActive(false);
    }

    public void Back()
    {
        //True
        carpetaInicial.SetActive(true);
        //False
        carpetaCredits.SetActive(false);
        carpetaConfig.SetActive(false);
    }

}