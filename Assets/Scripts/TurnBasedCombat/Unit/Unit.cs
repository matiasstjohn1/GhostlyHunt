using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
	private Animator anim;

	//Cambiar personaje
	private Image image;
	public List<Sprite> sprites;

    public List<string> unitName;

    public float currentHP;

	//Random//
	Dictionary<AttackEnum, int> _names;
	public List<AttackInfo> nameInfo;
	public int nameIndex;

	//Random Enemy Stats//
	public int randomLvl;
	public int randomDamage;
	public float randomMaxHP;

	private void Awake()
	{
		_names = new Dictionary<AttackEnum, int>();
		for (int i = 0; i < nameInfo.Count; i++)
		{
			var curr = nameInfo[i];
			_names[curr.names] = curr.weight;
		}
		nameIndex=GetRandomName();
		randomLvl= Random.Range(10, 101);
		randomDamage = Random.Range(10, 51);
		randomMaxHP = Random.Range(100, 1001);
		currentHP = randomMaxHP;
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

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > randomMaxHP)
			currentHP = randomMaxHP;
	}

	public void ChangeSprite(int index)
	{
		if (index >= 0 && index < sprites.Count)
		{
			image.sprite = sprites[index];
		}
		else
		{
			Debug.LogError("No hay sprites en Lista.");
		}
	}

	public int GetRandomName()
	{
		var rarity = MyRandoms.Roulette(_names); //Uso del My Random.
		return (int)rarity;
	}
}
