using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partner : MonoBehaviour
{
    //Variables pre definidas.//
    SpriteRenderer spriteRenderer;
    //Sigue al jugador la camara con una velocidad elegida por nosotros.
    [SerializeField] GameObject Jugador;
    [SerializeField] float vel;
    private bool isFacingRight = false;
    public List<Sprite> sprites; //Acompañantes.

    Vector3 newPos;

    private void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        newPos = Vector3.Lerp(transform.position, Jugador.transform.position, vel * Time.deltaTime);

        if (transform.position.x < Jugador.transform.position.x && !isFacingRight)
        {
            LookDir();
        }
        else if (transform.position.x > Jugador.transform.position.x && isFacingRight)
        {
            LookDir();
        }

        transform.position = newPos;
        ChangeSprite(StatsManager.Instance._index);
    }

    public void ChangeSprite(int index)
    {
       spriteRenderer.sprite = sprites[index];
    }

    public void LookDir()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
