using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public class BattleHUD : MonoBehaviour
{
	//Variables enemy//
	public Text nameText;
	public Text levelText;
	public Image hpSlider;
	public Text damageText;
	public Text HP;
	private Unit _unit;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName[unit.nameIndex];
		levelText.text = "Lvl: " + unit.randomLvl; //Nivel enemy.
		damageText.text = "Damage: " + unit.randomDamage; //Daño enemy
	}

	private void Update()
	{
		
		_unit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();
        nameText.text = _unit.unitName[_unit.nameIndex];
        hpSlider.fillAmount = _unit.currentHP / _unit.randomMaxHP;
		HP.text = "HP: " + (int)_unit.currentHP;
	}

	public void SetHP(float hp)
	{
		hpSlider.fillAmount = hp;
	}

}
