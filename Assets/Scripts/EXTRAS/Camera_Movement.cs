using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{

    //Sigue al jugador la camara con una velocidad elegida por nosotros.
    [SerializeField] GameObject Jugador;
    [SerializeField] float vel;

    Vector3 newPos;

    private void Update()
    {
        newPos = Vector3.Lerp(transform.position, Jugador.transform.position, vel * Time.deltaTime);
        newPos.z = -10;
        
        transform.position = newPos;
    }
}