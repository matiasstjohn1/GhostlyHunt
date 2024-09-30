using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesItems : MonoBehaviour
{
    [SerializeField] int amount;
    [SerializeField] StatType type;
    [SerializeField] int itemID; // Identificador único del objeto

    public int cantidad;
    private InventroyManager inventoryManager;
    private BattleSystem battleSystem;

    private void Start()
    {
        GameObject inventory = GameObject.FindGameObjectWithTag("InventarioM");
        if (inventory != null)
        {
            inventoryManager = inventory.GetComponent<InventroyManager>();
        }
        battleSystem = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleSystem>();
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
