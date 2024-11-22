using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnlockableItem : MonoBehaviour
{
    [SerializeField] private int unlockIndex;
    private void Start()
    {

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
    }
}
