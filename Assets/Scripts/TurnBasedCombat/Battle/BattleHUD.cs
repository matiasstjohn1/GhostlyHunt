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
	private UnitBoss _boss;

	public void SetHUD(Unit unit)
	{
		_unit = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Unit>();
		nameText.text = unit.unitName[unit.nameIndex];
		levelText.text = "Lvl: " + unit.randomLvl; //Nivel enemy.
		damageText.text = "Damage: " + unit.randomDamage; //Daño enemy
	}

	public void SetBossHUD(UnitBoss unit)
	{
		_boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<UnitBoss>();
		nameText.text = unit.unitName;
		levelText.text = "Lvl: " + unit.Lvl; //Nivel enemy.
		damageText.text = "Damage: " + unit.Damage; //Daño enemy
	}

	private void Update()
	{
		if(_boss != null)
		{
			nameText.text = _boss.unitName;
			hpSlider.fillAmount = _boss.currentHP / _boss.MaxHP;
			HP.text = "HP: " + (int)_boss.currentHP;
		}
		if (_unit != null)
		{
			nameText.text = _unit.unitName[_unit.nameIndex];
			hpSlider.fillAmount = _unit.currentHP / _unit.randomMaxHP;
			HP.text = "HP: " + (int)_unit.currentHP;
		}
	}

	public void SetHP(float hp)
	{
		hpSlider.fillAmount = hp;
	}

}