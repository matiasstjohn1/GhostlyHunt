using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostlySave : MonoBehaviour
{
    public static GhostlySave Instance;

    public int slotId;
    private BattleSystem battleSystem;
    private InventoryGh inventoryGh;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        battleSystem = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleSystem>();

        GameObject inventory = GameObject.FindGameObjectWithTag("InventarioM");
        if (inventory != null)
        {
            inventoryGh = inventory.GetComponent<InventoryGh>();
        }
    }

    void Update()
    {
        
    }

    public void Ghostly1()
    {
        AudioManager.instance.PlayCombatSounds(8);

        if (battleSystem != null && battleSystem.Statsinfo != null && battleSystem.Statsinfo.Count > 0)
        {
            var statInfo = battleSystem.Statsinfo[slotId];
            Debug.Log("damage" + statInfo._damage);
            Debug.Log("HP" + statInfo._HPmax);
            Debug.Log("LVL" + statInfo._lvl);

            StatsSave.Instance._nameIndex1 = StatsSave.Instance.cleanSlotId;
            StatsSave.Instance._damage1 = statInfo._damage;
            StatsSave.Instance._HPmax1 = statInfo._HPmax;
            StatsSave.Instance._lvl1 = statInfo._lvl;
            StatsSave.Instance.currentHealth1 = statInfo._HPmax;
            battleSystem.i -= 1;
        }

        if (inventoryGh != null)
        {
            inventoryGh.removeItems(StatsSave.Instance.cleanSlotId);
        }
    }
    public void Ghostly2()
    {
        AudioManager.instance.PlayCombatSounds(8);

        if (battleSystem != null && battleSystem.Statsinfo != null && battleSystem.Statsinfo.Count > 0)
        {
            var statInfo = battleSystem.Statsinfo[slotId];
            Debug.Log("damage" + statInfo._damage);
            Debug.Log("HP" + statInfo._HPmax);
            Debug.Log("LVL" + statInfo._lvl);

            StatsSave.Instance._nameIndex2 = StatsSave.Instance.cleanSlotId;
            StatsSave.Instance._damage2 = statInfo._damage;
            StatsSave.Instance._HPmax2 = statInfo._HPmax;
            StatsSave.Instance._lvl2 = statInfo._lvl;
            StatsSave.Instance.currentHealth2 = statInfo._HPmax;
            battleSystem.i -= 1;
        }

        if (inventoryGh != null)
        {
            inventoryGh.removeItems(StatsSave.Instance.cleanSlotId);
        }
    }
    public void Ghostly3()
    {
        AudioManager.instance.PlayCombatSounds(8);

        if (battleSystem != null && battleSystem.Statsinfo != null && battleSystem.Statsinfo.Count > 0)
        {
            var statInfo = battleSystem.Statsinfo[slotId];
            Debug.Log("damage" + statInfo._damage);
            Debug.Log("HP" + statInfo._HPmax);
            Debug.Log("LVL" + statInfo._lvl);

            StatsSave.Instance._nameIndex3 = StatsSave.Instance.cleanSlotId;
            StatsSave.Instance._damage3 = statInfo._damage;
            StatsSave.Instance._HPmax3 = statInfo._HPmax;
            StatsSave.Instance._lvl3 = statInfo._lvl;
            StatsSave.Instance.currentHealth3 = statInfo._HPmax;
            battleSystem.i -= 1;
        }

        if (inventoryGh != null)
        {
            inventoryGh.removeItems(StatsSave.Instance.cleanSlotId);
        }
    }

    public void Freedom()
    {
        AudioManager.instance.PlayCombatSounds(9);//CAMBIAR SONIDO

        if (inventoryGh != null)
        {
            inventoryGh.removeItems(StatsSave.Instance.cleanSlotId);
        }
    }
}
