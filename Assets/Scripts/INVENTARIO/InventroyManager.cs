using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventroyManager : MonoBehaviour
{
    public GameObject[] slots; //Array de los slots.
    Text text;
    private int maxSlots = 6; //Máximo de slots en el inventario (en canvas 0 al 6).
    public List<GameObject> canvasSlots;
    public bool fullInv = false;
    public GameObject invFullText;

    void Start()
    {
        slots = new GameObject[maxSlots];
    }

    public GameObject[] getSlots()
    {
        return this.slots;
    }

    public void setSlots(GameObject slot, int pos, int cant, int itemID)
    {
        bool found = false;
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].GetComponent<AttributesItems>().getID() == itemID)
            {
                int already_cant = slots[i].GetComponent<AttributesItems>().getCantidad();
                slots[i].GetComponent<AttributesItems>().setCantidad(already_cant + cant);

                // Actualiza el texto de la cantidad en el canvas slot
                UpdateSlotText(i, already_cant + cant);

                found = true;
                break;
            }
        }

        if (!found)
        {
            slot.GetComponent<AttributesItems>().setCantidad(cant);
            this.slots[pos] = slot;
            UpdateSlotText(pos, cant);
        }
    }

    public void showInventory()
    {
        if (canvasSlots != null && canvasSlots.Count > 0)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                GameObject slotObject = canvasSlots[i];
                if (slotObject != null)
                {
                    if (slots[i] != null)
                    {
                        if (slotObject.transform.childCount == 0)
                        {
                            GameObject item = Instantiate(slots[i], slotObject.transform.position, Quaternion.identity);
                            item.transform.SetParent(slotObject.transform, false);
                            item.transform.localPosition = Vector3.zero;
                            item.name = item.name.Replace("(Clone)", "");

                            Text text = item.GetComponentInChildren<Text>();
                            int cant = slots[i].GetComponent<AttributesItems>().getCantidad();
                            text.text = cant.ToString();
                        }
                        else
                        {
                            // Actualiza la cantidad mostrada en el texto
                            UpdateSlotText(i, slots[i].GetComponent<AttributesItems>().getCantidad());
                        }
                    }
                    else
                    {
                        // Si no hay un objeto en el slot correspondiente, limpiamos el slot en el Canvas
                        if (slotObject.transform.childCount > 0)
                        {
                            Destroy(slotObject.transform.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
    }

    public bool removeItems(int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] != null && slots[i].GetComponent<AttributesItems>().getID() == itemID)
            {
                int cantidad = slots[i].GetComponent<AttributesItems>().getCantidad();
                if (cantidad > 1)
                {
                    slots[i].GetComponent<AttributesItems>().setCantidad(cantidad - 1);

                    //Actualiza el texto de la cantidad en el canvas slot
                    UpdateSlotText(i, cantidad - 1);
                }
                else
                {
                    //Elimina el objeto visualmente del canvas
                    if (canvasSlots[i].transform.childCount > 0)
                    {
                        Destroy(canvasSlots[i].transform.GetChild(0).gameObject);
                    }
                    slots[i] = null;
                }
                return true;
            }
        }
        return false;
    }

    private void UpdateSlotText(int slotIndex, int cantidad)
    {
        if (canvasSlots[slotIndex] != null && canvasSlots[slotIndex].transform.childCount > 0)
        {
            Text text = canvasSlots[slotIndex].transform.GetChild(0).GetComponentInChildren<Text>();
            text.text = cantidad.ToString();
        }
    }
    public void FullInventory(int value)
    {
        fullInv = true;
    }   
}
