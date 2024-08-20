using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coleccionable : MonoBehaviour
{
    //Colision del coleccionable
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            //GameManager.Instance.collectable += 1;//Sumamos 1 a la variable collectable que esta en el GameManager.
            Destroy(gameObject);
        }
        
    }
}
