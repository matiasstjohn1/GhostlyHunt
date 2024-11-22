using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Dialogue : MonoBehaviour
{
    public Dialogue dialogue;

    //Variables del codigo.
    private Collider2D z_Collider;  //Colision del objeto que podes clickear
    [SerializeField] GameObject Interaccion; //Texto que sale para interactuar
    [SerializeField]
    private ContactFilter2D z_Filter; //Filtro de lo que tiene que chocar para activarse.
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    public int p = 0;
    public bool seActivo = false;

    //Llamo al player//
    private Movement_Main movement;

    //Unlock.
    [SerializeField] private int unlockIndex;

    void Start()
    {
        z_Collider = GetComponent<Collider2D>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement_Main>();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Update()
    {
        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);
        foreach (var o in z_CollidedObjects)
        {
            
            if (Input.GetKeyDown(KeyCode.F)&&seActivo==false)
            {
                p++;
                UnlocksManager.instance.UnlockByIndex(unlockIndex);
                TriggerDialogue();
                movement.velocidadMovimiento = 0;
                Interaccion.SetActive(false);
                GameManager.Instance.obv4 = true;
                seActivo = true;
                
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                
            }
            if (p == 0)
            {
                movement.SaveStat();
            }
        }

        if (FindObjectOfType<DialogueManager>().finishDialogue&&seActivo)
        {
            seActivo = false;
            p = 0;
            movement.velocidadMovimiento = movement.velmovsave;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Interaccion.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Interaccion.SetActive(false);
    }
}