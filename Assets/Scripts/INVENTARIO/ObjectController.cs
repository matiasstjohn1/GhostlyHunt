using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public GameObject obj;
    public int cant=1;
    public int itemID; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject[] inventario = GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().getSlots();
            GameManager.Instance.obv1 = true;

            for (int i = 0; i < inventario.Length; i++)
            {


                if (!inventario[i]&& GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().fullInv == false)
                {
                    AudioManager.instance.PlayCombatSounds(4);
                    GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().setSlots(obj, i, cant, itemID);
                    GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().showInventory();
                    Destroy(gameObject);
                    break;
                }
                if (inventario[i].GetComponent<AttributesItems>().getID() == itemID)
                {
                    AudioManager.instance.PlayCombatSounds(4);
                    GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().setSlots(obj, i, cant, itemID);
                    GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().showInventory();
                    Destroy(gameObject);
                    break;
                }
                else if (inventario[i].GetComponent<AttributesItems>().getID() != itemID && GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().fullInv == true)
                {
                    GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().invFullText.SetActive(true);
                }
                if ((inventario.Length-1) <= i)
                {
                    GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().FullInventory(i);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().fullInv == true)
        {
            GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().invFullText.SetActive(false);
        }
    }
}
