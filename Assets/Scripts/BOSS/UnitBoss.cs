using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBoss : MonoBehaviour
{
	//Cambiar personaje
	private Image image;
	public Sprite sprites;

	public string unitName;

	public float currentHP;

	//Random Boss Stats//
	public int Lvl;
	public int Damage;
	public float MaxHP;

	private void Awake()
	{
		Lvl = 80;
		Damage = 35;
		MaxHP = 1000;
		currentHP = MaxHP;
	}

	private void Start()
	{
		image = GetComponent<Image>();
	}
	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}
}
