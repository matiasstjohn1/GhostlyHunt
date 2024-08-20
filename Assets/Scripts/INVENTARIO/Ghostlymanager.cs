using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostlymanager : MonoBehaviour
{
    [SerializeField] int amount;
    public int GhId; // Identificador único del objeto

    public int cantidad;

    public int slotIdM;
    private BattleSystem battleSystem;

    private void Awake()
    {
        battleSystem = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleSystem>();
        slotIdM = battleSystem.i-1;
    }

    public void Activar()
    {
        BackpackManager.Instance.A = 1;
        StatsSave.Instance.cleanSlotId = GhId;
        GhostlySave.Instance.slotId=slotIdM;
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
        return GhId;
    }
}
//GameManager.Instance.obv5 = true;

