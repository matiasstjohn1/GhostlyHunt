using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement_Main : MonoBehaviour
{
    //Variables jugador.

    [SerializeField] public float velocidadMovimiento; //Velocidad
    [SerializeField] private Vector2 direccion; //Direccion movimiento
    //[SerializeField] private AudioClip caminarSonido; futuro sonido
    private Rigidbody2D myRb;
    private float MoviX; //Lateral
    private float MoviY; //Vertical

    private float currentTime;
    private Animator animator;
    public float velmovsave;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        //audio = GetComponent<AudioSource>(); futuro sonido
    }

    void Update()
    {
        //Movimiento del jugador.
        MoviX = Input.GetAxisRaw("Horizontal");
        MoviY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MovX", MoviX);
        animator.SetFloat("MovY", MoviY);

        if (MoviX != 0 || MoviY != 0)
        {
            animator.SetFloat("UltX", MoviX);
            animator.SetFloat("UltY", MoviY);
        }

        direccion = new Vector2(MoviX, MoviY).normalized;

        currentTime += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Escena de victoria
        if (collision.gameObject.layer == 9)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void FixedUpdate()
    {
        myRb.MovePosition(myRb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
    }

    public void SaveStat()
    {
        velmovsave = velocidadMovimiento;
    }
}
