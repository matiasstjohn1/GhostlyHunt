using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int _random;
    public int a;

    //Temporizador//
    private float currentTime;
    public float tiempoCombat;

    //Sistema de Combate//
    BattleSystem battle;
    [SerializeField] GameObject imageBattale;

    //Random//
    Dictionary<AttackEnum, int> _names;
    public List<AttackInfo> nameInfo;
    public int nameIndex;

    void Start()
    {
        battle = GameObject.FindGameObjectWithTag("Battle").GetComponent<BattleSystem>();
    }

    void Update()
    {
        _names = new Dictionary<AttackEnum, int>();
        for (int i = 0; i < nameInfo.Count; i++)
        {
            var curr = nameInfo[i];
            _names[curr.names] = curr.weight;
        }

        currentTime += Time.deltaTime;
        if (currentTime >= 2)
        {
            _random = (int)MyRandoms.Range(0, 11);
            currentTime = 0;
        }
        tiempoCombat += Time.deltaTime;
    }

    //Colision player spawner, activa combate.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (tiempoCombat >= 3)
            {
                if (_random == 6 || _random == 7 || _random == 8 || _random == 9 || _random == 10)
                {
                    Debug.Log(_random);
                    if (a == 0)
                    {
                        BackpackManager.Instance.fondoItems.SetActive(false);
                        BackpackManager.Instance.slotsItems.SetActive(false);
                        BackpackManager.Instance.slotsGh.SetActive(false);
                        imageBattale.SetActive(true);
                        battle.SetUpC();
                        a = 1;
                    }

                }
            }

        }
    }
    public int GetRandomName()
    {
        var rarity = MyRandoms.Roulette(_names); //Uso del My Random.
        return (int)rarity;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            a = 0;
        }
    }
}