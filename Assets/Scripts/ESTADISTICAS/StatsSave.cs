using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsSave : MonoBehaviour
{
    public static StatsSave Instance;

    ////Guarda todas las variables de cada Ghostly en tu equipo.//
    ////Vars personaje 0.//
    public int _nameIndex0;
    public int _damage0; //Daño de la unidad.
    public float _HPmax0; //Vida maxima unidad.
    public int _lvl0; //Nivel a guardar.
    public int _xp0; //Xp a guardar.
    public int stamina0; //Stamina a guardar

    //Vars personaje 1. Firy//
    public int _nameIndex1;
    public int _damage1; //Daño de la unidad.
    public float _HPmax1; //Vida maxima unidad.
    public int _lvl1; //Nivel a guardar.
    public int _xp1; //Xp a guardar.
    public int stamina1; //Stamina a guardar

    //Vars personaje 2. Aquafina//
    public int _nameIndex2;
    public int _damage2; //Daño de la unidad.
    public float _HPmax2; //Vida maxima unidad.
    public int _lvl2; //Nivel a guardar.
    public int _xp2; //Xp a guardar.
    public int stamina2; //Stamina a guardar

    //Vars personaje 3. Kronk//
    public int _nameIndex3;
    public int _damage3; //Daño de la unidad.
    public float _HPmax3; //Vida maxima unidad.
    public int _lvl3; //Nivel a guardar.
    public int _xp3; //Xp a guardar.
    public int stamina3; //Stamina a guardar

    public int cleanSlotId;//Detecta ID del inv Ghostly paara saber qn borrar.

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }
   
    

}
