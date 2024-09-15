using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    public int a;

    //Sistema de Combate//
    BattleSystem battle;
    [SerializeField] GameObject imageBattale; 

    void Start()
    {
        battle = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleSystem>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           if (a == 0)
           {
             BackpackManager.Instance.fondoItems.SetActive(false);
             BackpackManager.Instance.slotsItems.SetActive(false);
             BackpackManager.Instance.slotsGh.SetActive(false);
             imageBattale.SetActive(true);
             battle.SetUpB();
             a = 1;
           }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            a = 0;
        }
    }
}
