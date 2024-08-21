using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance;

    public GameObject botonItems; 
    public GameObject botonGhostly; //Boton Ghostlys
    public GameObject botonX; //Boton X
    public GameObject botonX2; //Boton X2
    public GameObject botonObjetivos; //Boton objetivos (provisional)
    public GameObject fondoMochila; //Mochila fondo
    public GameObject objectives; //Objetivos

    //Inventario Items//.
    public GameObject fondoItems;
    public GameObject slotsItems;

    //Inventario Ghostly//.
    public GameObject slotsGh;

    //Diario//
    public GameObject diarioGeneral;
    public GameObject diarioGhostlys;
    //Botones diario//
    public GameObject diarioItems;
    public GameObject diarioNpcs;
    //Categorias//
    public GameObject categoryGhostlys;
    public GameObject categoryItems;
    public GameObject categoryNpcs;

    public bool activeItem = true;
    public bool activeGhostly = true;
    public bool activeMapa = true;
    public bool activeBack = true;

    //Diario paginas items//
    public GameObject flecha; //Boton siguiente
    public GameObject flechaBack; //Boton atras
    public GameObject categoryItems2;

    //Dirario paginas Npcs//
    public GameObject flechaNpc; //Boton siguiente
    public GameObject flechaBackNpc; //Boton atras
    public GameObject categoryNpcs2;

    //Para elegir ghostly//
    public GameObject botonesGhostly;
    public int A = 0;
    public BattleSystem BattleSystem;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (A == 1)
        botonesGhostly.SetActive(true);
    }

    public void ButtonItems()
    {
        //GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().showInventory();
        fondoItems.SetActive(true);
        slotsItems.SetActive(true);
        botonX.SetActive(true);
        slotsGh.SetActive(false);
        objectives.SetActive(false);
        diarioGeneral.SetActive(false);
        botonX2.SetActive(false);
        A = 0;
        botonesGhostly.SetActive(false);
        
    }
    public void ButtonGhostlys()
    {
        //GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventoryGh>().showInventory();
        fondoItems.SetActive(true);
        slotsGh.SetActive(true);
        botonX.SetActive(true);
        slotsItems.SetActive(false);
        objectives.SetActive(false);
        diarioGeneral.SetActive(false);
        botonX2.SetActive(false);
    }
    public void ButtonX()
    {
        fondoItems.SetActive(false);
        slotsGh.SetActive(false);
        slotsItems.SetActive(false);
        objectives.SetActive(false);
        diarioGeneral.SetActive(false);
        botonX.SetActive(false);
        botonX2.SetActive(false);
        flecha.SetActive(false);
        categoryItems2.SetActive(false);
        categoryNpcs2.SetActive(false);
        A = 0;
        BattleSystem.c = 0;
        BattleSystem.b = 0;
        botonesGhostly.SetActive(false);
        flechaBack.SetActive(false);
        flechaNpc.SetActive(false);
        flechaBackNpc.SetActive(false);
    }

    public void CancelGhostlyChange()
    {
        A = 0;
        botonesGhostly.SetActive(false);
    }


    ///DIARIO SECCION///
    public void ButtonDiario()
    {
        fondoItems.SetActive(false);
        slotsGh.SetActive(false);
        slotsItems.SetActive(false);
        objectives.SetActive(false);

        diarioGeneral.SetActive(true);
        diarioGhostlys.SetActive(true);
        diarioItems.SetActive(true);
        diarioNpcs.SetActive(true);
        botonX2.SetActive(true);

        categoryItems.SetActive(false);
        categoryItems2.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);
        botonX.SetActive(false);
        flecha.SetActive(false);
        flechaBack.SetActive(false);
        flechaNpc.SetActive(false);
        flechaBackNpc.SetActive(false);
        categoryNpcs2.SetActive(false);

    }
    public void ButtonObjectives()
    {
        fondoItems.SetActive(false);
        slotsGh.SetActive(false);
        slotsItems.SetActive(false);
        diarioGeneral.SetActive(false);
        A = 0;
        botonesGhostly.SetActive(false);

        objectives.SetActive(true);
        botonX2.SetActive(true);
        botonX.SetActive(false);
    }
    public void ButtonDiarioGhostly()
    {
        diarioGhostlys.SetActive(false);
        diarioItems.SetActive(false);
        diarioNpcs.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryItems.SetActive(false);
        categoryItems2.SetActive(false);

        categoryGhostlys.SetActive(true);
        botonX2.SetActive(true);
    }
    public void ButtonDiarioItems()
    {
        diarioGhostlys.SetActive(false);
        diarioItems.SetActive(false);
        diarioNpcs.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);

        botonX2.SetActive(true);
        categoryItems.SetActive(true);
        flecha.SetActive(true);

        categoryItems2.SetActive(false);
        flechaBack.SetActive(false);
    }
    public void ButtonDiarioItems2()
    {
        diarioGhostlys.SetActive(false);
        diarioItems.SetActive(false);
        diarioNpcs.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);

        botonX2.SetActive(true);
        categoryItems.SetActive(false);
        flecha.SetActive(false);

        categoryItems2.SetActive(true);
        flechaBack.SetActive(true);
    }
    public void ButtonDiarioNpcs()
    {
        diarioGhostlys.SetActive(false);
        diarioItems.SetActive(false);
        diarioNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);
        categoryItems.SetActive(false);
        flechaBackNpc.SetActive(false);
        categoryNpcs2.SetActive(false);

        botonX2.SetActive(true);
        categoryNpcs.SetActive(true);
        flechaNpc.SetActive(true);
    }
    public void ButtonDiarioNpcs2()
    {
        diarioGhostlys.SetActive(false);
        diarioItems.SetActive(false);
        diarioNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);
        categoryItems.SetActive(false);
        categoryNpcs.SetActive(false);
        flechaNpc.SetActive(false);

        botonX2.SetActive(true);
        flechaBackNpc.SetActive(true);
        categoryNpcs2.SetActive(true);
    }
}
