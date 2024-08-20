using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variables que usaremos.

    public static GameManager Instance;
    [SerializeField] GameObject player; //Personaje
    public float points = 0; //Texto de puntos
    public int pj; //Player a seleccion
    public GameObject inv; //Inventario player
    public GameObject invItems; //Inventario player
    public GameObject invGhostlys; //Inventario player
    public GameObject fondoInvs; //Inventario player
    public bool invActivo = true;
    public GameObject Final;

    //Objetivos (solo para entrega del 28 luego eliminar).//
    public GameObject obv; //Menu objetivo en Canvas.
    public bool obvActivo = true;
    public bool obv1 = false; //Obtener item.
    public bool obv2 = false; //Ganar combate.
    public bool obv3 = false; //Capturar ghostly.
    public bool obv4 = false; //Hablar con Npc.
    public bool obv5 = false; //Cambiar Ghostly.
    //Texto para objetivos//.
    public Text obvText1;
    public Text obvText2;
    public Text obvText3;
    public Text obvText4;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    private void Update()
    {
        //PASAR OBJETIVOS Y GANAR//////////////////
        if(obv1)
        {
            obvText1.text = "Obtener un Item=1";
        }
        if (obv2)
        {
            obvText2.text = "Ganar un combate=1";
        }
        if (obv3)
        {
            obvText3.text = "Capturar un Ghostly=1";
        }
        if (obv4)
        {
            obvText4.text = "Hablar con un Npc=1";
        }
        if(obv1&&obv2&&obv3&&obv4)
        {
            Final.SetActive(false);
        }
        ///////////////////////////////////////////
    }

    public void CharacterPassAway()
    {
        SceneManager.LoadScene(3);
    }
}
