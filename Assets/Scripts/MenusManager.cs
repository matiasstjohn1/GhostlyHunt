using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenusManager : MonoBehaviour
{
    //Seccion inicio//
    public GameObject botonConfig; //Boton de creditos.
    public GameObject botonCredits; //Boton de configuracion.
    public GameObject back; //Boton para atras.

    public GameObject botonTeclas; //Boton de configuracion --> teclas.
    public GameObject botonGraficos; //Boton de configuracion --> graficos.
    public GameObject botonVolumen; //Boton de configuracion --> volumen.

    public GameObject carpetaInicial;
    public GameObject carpetaConfig;
    public GameObject carpetaCredits;

    public GameObject carpetaConfigTeclas;
    public GameObject carpetaConfigGraficos;
    public GameObject carpetaConfigVolumen;

    public void Config()
    {
        //True
        carpetaConfig.SetActive(true);
        carpetaConfigTeclas.SetActive(true);
        //False
        carpetaCredits.SetActive(false);
        carpetaInicial.SetActive(false);
        carpetaConfigGraficos.SetActive(false);
        carpetaConfigVolumen.SetActive(false);
    }

    public void Credits()
    {
        //True
        carpetaCredits.SetActive(true);
        //False
        carpetaConfig.SetActive(false);
        carpetaInicial.SetActive(false);
    }

    public void InConfigurationTeclas()
    {
        //True
        carpetaConfigTeclas.SetActive(true);
        //False
        carpetaConfigGraficos.SetActive(false);
        carpetaConfigVolumen.SetActive(false);
    }
    public void InConfigurationGraficos()
    {
        //True
        carpetaConfigGraficos.SetActive(true);
        //False
        carpetaConfigTeclas.SetActive(false);
        carpetaConfigVolumen.SetActive(false);
    }
    public void InConfigurationVolumen()
    {
        //True
        carpetaConfigVolumen.SetActive(true);
        //False
        carpetaConfigTeclas.SetActive(false);
        carpetaConfigGraficos.SetActive(false);
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