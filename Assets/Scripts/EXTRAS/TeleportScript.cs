using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    //Variables de TP
    public Transform teleportTarget;  //Referencia al objeto de destino
    public Transform Player; //Quien se hace TP (posicion del jugador)
    public Transform Camara; //Quien se hace TP (posicion de la camara)
    public Transform Compa; //Quien se hace TP (posicion del partner)

    //Permite realizar el TP segun colision del jugador
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 3)
        {
            // Teletransportar al jugador al destino
            Player.position = teleportTarget.position;
            Camara.position = teleportTarget.position;
            Compa.position = teleportTarget.position;
        }
    }
}
