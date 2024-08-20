using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHudP : MonoBehaviour
{

	//Variable player//
	public Text nameTextP;
	public Text levelTextP;
	public Image hpSliderP;
	public Text damageTextP;
	public Text HPP;
	private UnitP _unitp;
	public Text stamina;

	public void SetHUDP(UnitP unitp)
	{
		nameTextP.text = unitp.unitName[StatsManager.Instance._index];
		levelTextP.text = "Lvl: " + StatsManager.Instance._unitLevel; 
		damageTextP.text = "Damage: " + StatsManager.Instance._damage;
		stamina.text = "Stamina: "+ StatsManager.Instance.stamina;
	}

    private void Update()
    {
		_unitp= GameObject.FindGameObjectWithTag("Unit").GetComponent<UnitP>();
		hpSliderP.fillAmount = _unitp.currentHP / StatsManager.Instance._maxHP;
		HPP.text = "HP: " + _unitp.currentHP;
		stamina.text = "Stamina: " + StatsManager.Instance.stamina;
	}

	public void SetHP(float hp)
	{
		hpSliderP.fillAmount = hp;
	}
}
