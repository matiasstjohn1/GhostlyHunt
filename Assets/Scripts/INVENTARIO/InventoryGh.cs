using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGh : MonoBehaviour
{
    public GameObject[] slots; // Array de los slots.
    Text text;
    private int maxSlots = 6; // Máximo de slots en el inventario (en canvas 0 al 19).
    public List<GameObject> canvasSlots;

    void Start()
    {
        slots = new GameObject[maxSlots];
    }

    public GameObject[] getSlots()
    {
        Debug.Log("GetSlots"+this.slots.Length);
        return this.slots;
    }

    public void setSlots(GameObject slot, int pos, int cant, int itemID)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slot.GetComponent<Ghostlymanager>().setCantidad(cant);
                this.slots[i] = slot;
                UpdateSlotText(i, cant);
                return;
            }
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
                            int cant = slots[i].GetComponent<Ghostlymanager>().getCantidad();
                            text.text = cant.ToString();
                        }
                        else
                        {
                            // Actualiza la cantidad mostrada en el texto
                            UpdateSlotText(i, slots[i].GetComponent<Ghostlymanager>().getCantidad());
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
            if (slots[i] != null && slots[i].GetComponent<Ghostlymanager>().getID() == itemID)
            {
                int cantidad = slots[i].GetComponent<Ghostlymanager>().getCantidad();
                if (cantidad > 1)
                {
                    slots[i].GetComponent<Ghostlymanager>().setCantidad(cantidad - 1);

                    // Actualiza el texto de la cantidad en el canvas slot
                    UpdateSlotText(i, cantidad - 1);
                }
                else
                {
                    // Elimina el objeto visualmente del canvas
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
}
