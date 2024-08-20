using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public int lvl; //Siguiente nivel que sube tus stats.
    public int xp1; //Xp necesaria para subir nivel personaje 1.
    public int xp2; //Xp necesaria para subir nivel personaje 2.
    public int xp3; //Xp necesaria para subir nivel personaje 3.

    public int personaje0=0;
    public int personaje1=1;
    public int personaje2=2;

    //Vars a modificar//
    public int _unitLevel; //Nivel de unidad.
    public int _damage; //Daño de la unidad.
    public float _maxHP; //Vida maxima unidad.
    public int _unitXp; //Unidad xp actual.
    public int stamina;//Indica cuanta stamina usa de ataque.

    public int _index; //Va para nombre e imagen por igual.

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    private void Start()
    {
        lvl = 5;
        xp1 = 1500;
        xp2= 1500;
        xp3 = 6000;

        _unitLevel = StatsSave.Instance._lvl1;
        _damage = StatsSave.Instance._damage1;
        _maxHP = StatsSave.Instance._HPmax1;
        StatsSave.Instance._xp1 = _unitXp;
        stamina = 100;
    }

    void Update()
    {
        
       if((GameManager.Instance.pj)==personaje0)
       {
            lvl = 10;
            _index = StatsSave.Instance._nameIndex1;
            _unitLevel = StatsSave.Instance._lvl1;
            _damage = StatsSave.Instance._damage1;
            _maxHP = StatsSave.Instance._HPmax1;
            StatsSave.Instance.stamina1 = stamina;
            //stamina = StatsSave.Instance.stamina1;
        }

        if ((GameManager.Instance.pj) == personaje1)
       {
            lvl = 10;
            _index = StatsSave.Instance._nameIndex2;
            _unitLevel = StatsSave.Instance._lvl2;
            _damage = StatsSave.Instance._damage2;
            _maxHP = StatsSave.Instance._HPmax2;
            StatsSave.Instance.stamina2 = stamina;
            //stamina = StatsSave.Instance.stamina2;
        }

        if ((GameManager.Instance.pj) == personaje2)
        {
           lvl = 20;
           _index = StatsSave.Instance._nameIndex3;
           _unitLevel = StatsSave.Instance._lvl3;
           _damage = StatsSave.Instance._damage3;
           _maxHP = StatsSave.Instance._HPmax3;
            StatsSave.Instance.stamina3=stamina;
            //stamina = StatsSave.Instance.stamina3;
        }
    }

    public void Actualizar()
    {
        //Logica de subir nivel//

        //Personaje1.
        if (StatsSave.Instance._xp1 >= xp1)
        {
            StatsSave.Instance._lvl1 += 1;
            xp1 += 500;
            StatsSave.Instance._xp1 = 0;
            _unitXp = 0;
        }

        if (StatsSave.Instance._lvl1 >= lvl)
        {
            StatsSave.Instance._damage1 += 10;
            StatsSave.Instance._HPmax1 += 100;
            lvl += 5;
        }

        //Personaje2.
        if (StatsSave.Instance._xp2 >= xp2)
        {
            StatsSave.Instance._lvl2 += 1;
            xp2 += 500;
            StatsSave.Instance._xp2 = 0;
            _unitXp = 0;
        }

        if (StatsSave.Instance._lvl2 >= lvl)
        {
            StatsSave.Instance._damage2 += 10;
            StatsSave.Instance._HPmax2 += 100;
            lvl += 5;
        }

        //Personaje3.
        if (StatsSave.Instance._xp3 >= xp3)
        {
            StatsSave.Instance._lvl3 += 1;
            xp3 += 500;
            StatsSave.Instance._xp3 = 0;
            _unitXp = 0;
        }

        if (StatsSave.Instance._lvl3 >= lvl)
        {
            StatsSave.Instance._damage3 += 10;
            StatsSave.Instance._HPmax3 += 100;
            lvl += 5;
        }
    }
}
