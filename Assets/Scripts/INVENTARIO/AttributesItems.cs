using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesItems : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] StatType type;
    [SerializeField] int itemID; // Identificador único del objeto

    public int cantidad;
    private InventroyManager inventoryManager;
    private BattleSystem battleSystem;

    [Header("Info Item")]
    public GameObject prefabInfo;
    public Text textInfo;
    [SerializeField] string itemDescription;

    private void Start()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("InventarioM");
        if (inventory != null)
        {
            inventoryManager = inventory.GetComponent<InventroyManager>();
        }
        battleSystem = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleSystem>();

        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            prefabInfo = canvas.transform.Find("INVTEXTINFO").gameObject; // Cambia "NombreDelPrefabInfo" al nombre real de prefabInfo
            if (prefabInfo != null)
            {
                textInfo = prefabInfo.GetComponentInChildren<Text>();
            }
        }
    }

    public void ActiveItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        AudioManager.instance.PlayCombatSounds(7);
        if (type == StatType.vel)
        {
           player.GetComponent<Movement_Main>().velocidadMovimiento += amount;
        }
        if(type==StatType.heal)
        {
            StartCoroutine(battleSystem.PlayerHeal(amount));
        }
        if (type == StatType.xp)
        {
            StatsManager.Instance._unitXp += amount;
        }
        if (type == StatType.attack)
        {
            StatsManager.Instance.stamina += amount;
        }
        if (inventoryManager != null)
        {
            inventoryManager.removeItems(itemID);
            inventoryManager.fullInv = false;
        }
    }
    public void ActiveInfo()
    {
        //AudioManager.instance.PlayCombatSounds(7);
        prefabInfo.SetActive(true);
        if (textInfo != null)
        {
            textInfo.text = itemDescription;
        }
    }

    public enum StatType
    {
        attack,//stamina
        vel,
        xp,
        heal
    }

    public void setCantidad(int cantidad)
    {
        this.cantidad = cantidad;
    }
    public int getCantidad()
    {
        return this.cantidad;
    }
    public int getID()
    {
        return itemID;
    }
}
