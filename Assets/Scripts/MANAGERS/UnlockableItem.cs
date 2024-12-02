using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnlockableItem : MonoBehaviour
{
    [SerializeField] private int unlockIndex;
    public CustomEventExample _cust;
    private ObjectController _objectController;
    private void Start()
    {
        _objectController = GetComponent<ObjectController>();
    }

    public void UnlockItem()
    {
        UnlocksManager.instance.UnlockByIndex(unlockIndex);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            GetComponent<UnlockableItem>().UnlockItem();
            
        }
        if (other.tag == "Player")
        {
            GameObject[] inventario = GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().getSlots();
            GameManager.Instance.obv1 = true;

            for (int i = 0; i < inventario.Length; i++)
            {
                if (!inventario[i] && GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().fullInv == false)
                {
                    _cust.ItemsIDa = unlockIndex;
                    _cust.ObjectiveIDa = 1001;
                    _cust.OnLevelComplete();
                    break;
                }
                if (inventario[i].GetComponent<AttributesItems>().getID() == _objectController.itemID)
                {
                    _cust.ItemsIDa = unlockIndex;
                    _cust.ObjectiveIDa = 1001;
                    _cust.OnLevelComplete();
                    break;
                }
            }
        }
    }
}
