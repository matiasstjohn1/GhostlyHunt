using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitBoss : MonoBehaviour
{
	//Cambiar personaje
	private Image image;

	public string unitName;

	public float currentHP;

	//Random Boss Stats//
	public int Lvl;
	public int Damage;
	public float MaxHP;

	private void Awake()
	{
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
