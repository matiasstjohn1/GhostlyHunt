using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance;
    [Header("BOTONES")]
    public GameObject botonItems; 
    public GameObject botonGhostly; //Boton Ghostlys
    public GameObject botonX; //Boton X
    public GameObject botonX2; //Boton X2
    public GameObject botonX3; //Boton X3
    public GameObject botonObjetivos; //Boton objetivos (provisional)
    [Header("FONDOS Y OBJETIVOS")]
    public GameObject fondoMochila; //Mochila fondo
    public GameObject objectives; //Objetivos

    //Inventario Items//.
    public GameObject fondoItems;
    [Header("SLOTS DE INVS")]
    public GameObject slotsItems;
    public GameObject itemInfo;

    //Inventario Ghostly//.
    public GameObject slotsGh;

    [Header("SECCION DIARIO")]
    //Diario//
    public GameObject diarioGeneral;
    //Botones diario//
    public GameObject tapa;

    [Header("CATEGORIAS DIARIO")]
    //Categorias//
    public GameObject categoryGhostlys;
    public GameObject categoryGhostly2;
    public GameObject categoryItems;
    public GameObject categoryNpcs;

    public bool activeItem = true;
    public bool activeGhostly = true;
    public bool activeMapa = true;
    public bool activeBack = true;

    [Header("SECCION DIARIO ITEMS")]
    //Diario paginas items//
    public GameObject flecha; //Boton siguiente
    public GameObject flechaBack; //Boton atras
    [Header("SECCION DIARIO NPCS")]
    //Dirario paginas Npcs//
    public GameObject flechaNpc; //Boton siguiente
    public GameObject flechaBackNpc; //Boton atras
    public GameObject categoryNpcs2;

    [Header("CAMBIOS GHOSTLYS")]
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
        botonX3.SetActive(false);
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
        botonX3.SetActive(false);
    }
    public void ButtonX()
    {
        fondoItems.SetActive(false);
        slotsGh.SetActive(false);
        slotsItems.SetActive(false);
        itemInfo.SetActive(false);
        objectives.SetActive(false);
        diarioGeneral.SetActive(false);
        botonX.SetActive(false);
        botonX2.SetActive(false);
        botonX3.SetActive(false);
        flecha.SetActive(false);
        categoryGhostly2.SetActive(false);
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
        tapa.SetActive(true);
        botonX3.SetActive(true);

        botonX2.SetActive(false);
        categoryItems.SetActive(false);
        categoryGhostly2.SetActive(false);
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
        botonX3.SetActive(true);

        botonX2.SetActive(false);
        botonX.SetActive(false);
    }
    public void ButtonDiarioGhostly()
    {
        tapa.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryItems.SetActive(false);
        categoryGhostly2.SetActive(false);

        categoryGhostlys.SetActive(true);
        botonX2.SetActive(true);
        flecha.SetActive(true);

        flechaBack.SetActive(false);
        botonX3.SetActive(false);
        botonX.SetActive(false);
    }
    public void ButtonDiarioItems()
    {
        tapa.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);

        botonX2.SetActive(true);
        categoryItems.SetActive(true);
        
        flecha.SetActive(false);
        categoryGhostly2.SetActive(false);
        flechaBack.SetActive(false);
        botonX3.SetActive(false);
        botonX.SetActive(false); 
    }
    public void ButtonDiarioGhostly2()
    {
        tapa.SetActive(false);
        categoryNpcs.SetActive(false);
        categoryGhostlys.SetActive(false);

        botonX2.SetActive(true);
        categoryItems.SetActive(false);
        flecha.SetActive(false);

        categoryGhostly2.SetActive(true);
        flechaBack.SetActive(true);
        botonX3.SetActive(false);
        botonX.SetActive(false);
    }
    public void ButtonDiarioNpcs()
    {
        tapa.SetActive(false);
        categoryGhostlys.SetActive(false);
        categoryItems.SetActive(false);
        flechaBackNpc.SetActive(false);
        categoryNpcs2.SetActive(false);

        botonX2.SetActive(true);
        categoryNpcs.SetActive(true);
        flechaNpc.SetActive(true);
        botonX3.SetActive(false);
        botonX.SetActive(false);
    }
    public void ButtonDiarioNpcs2()
    {
        tapa.SetActive(false);
        categoryGhostlys.SetActive(false);
        categoryItems.SetActive(false);
        categoryNpcs.SetActive(false);
        flechaNpc.SetActive(false);

        botonX2.SetActive(true);
        flechaBackNpc.SetActive(true);
        categoryNpcs2.SetActive(true);
        botonX3.SetActive(false);
        botonX.SetActive(false);
    }
}