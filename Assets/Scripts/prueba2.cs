using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prueba2 : MonoBehaviour //Se llama asi porque probamos cosas y no se cambio.
{
    //Variables del codigo.
    private Collider2D z_Collider;  //Colision del objeto que podes clickear
    private GameObject Lectura; //Texto que sale para leer
    [SerializeField] GameObject Collect; //Activa coleccionables
    [SerializeField] GameObject Interaccion; //Texto que sale para interactuar
    [SerializeField]
    private ContactFilter2D z_Filter; //Filtro de lo que tiene que chocar para activarse.
    private List<Collider2D> z_CollidedObjects = new List<Collider2D>(1);

    [SerializeField] List<GameObject> lecturas = new List<GameObject>();
    public float currentTime;
    public int p = 0;
    public bool seActivo=false;

    //Llamo al player//
    private Movement_Main movement;
    void Start()
    {
        z_Collider = GetComponent<Collider2D>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement_Main>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        z_Collider.OverlapCollider(z_Filter, z_CollidedObjects);
        foreach(var o  in z_CollidedObjects)
        {   
            if(p==0)
            {
                movement.SaveStat();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                movement.velocidadMovimiento = 0;
                Collect.SetActive(true);
                Interaccion.SetActive(false);
                GameManager.Instance.obv4 = true;
                seActivo = true;
                currentTime = 10;
            }
        }

        if (lecturas.Count <= p)
        {
            seActivo = false;
            p = 0;
            movement.velocidadMovimiento = movement.velmovsave;
        }

        if (seActivo==true)
        {
            if (currentTime >= 8)
            {
               if(lecturas.Count >= p)
               {
                   lecturas[p].SetActive(false);
                   p++;
                   if(lecturas.Count > p)
                   {
                      lecturas[p].SetActive(true);
                      currentTime = 0;
                   }
                }
            }
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
