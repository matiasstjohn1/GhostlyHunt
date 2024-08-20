using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhController : MonoBehaviour
{
    public static GhController Instance;
    public GameObject[] obj;
    public int cant = 1;
    public int GhID;
    private Unit unit;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }
        unit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();
    }


    public void colocarInv()
    {
        Debug.Log("Index:" + unit.nameIndex);
        AudioManager.instance.PlaySound(5);
        GameObject[] inventario = GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventoryGh>().getSlots();


        for (int i = 0; i < inventario.Length; i++)
        {
            Debug.Log("Inventario:" + inventario.Length);

            if (!inventario[i])
            {
                Debug.Log("paso1");
                GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventoryGh>().setSlots(obj[unit.nameIndex], i, cant, GhID);
                    //Destroy(gameObject);
                    break;
            }
            else if (inventario[i].GetComponent<Ghostlymanager>().getID() == GhID)
            {
                Debug.Log("paso2");
                GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventoryGh>().setSlots(obj[unit.nameIndex], i, cant, GhID);
                    //Destroy(gameObject);
                    break;
            }

        }
        
    }
}
