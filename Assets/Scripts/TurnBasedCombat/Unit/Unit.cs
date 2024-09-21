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

	public GameObject[] _spawner;
	public GameObject player;
	public int nameIndex;
	//Random Enemy Stats//
	public int randomLvl;
	public int randomDamage;
	public float randomMaxHP;

	private void Awake()
	{
		// Encuentra todos los spawners y el jugador
		GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		GameObject closest = null;
		float closestDistance = Mathf.Infinity;

		// Itera sobre todos los spawners para encontrar el más cercano
		foreach (GameObject spawner in spawners)
		{
			Vector3 spawnerPosition = spawner.transform.position;
			Vector3 playerPosition = player.transform.position;
			Vector3 diff = spawnerPosition - playerPosition;
			float distance = diff.sqrMagnitude;

			// Compara la distancia para encontrar el spawner más cercano
			if (distance < closestDistance)
			{
				closest = spawner;
				closestDistance = distance;
			}
		}

		// Si se ha encontrado un spawner más cercano, actualiza su nombreIndex
		if (closest != null)
		{
			Spawner spawnerComponent = closest.GetComponent<Spawner>();
			spawnerComponent.nameIndex = spawnerComponent.GetRandomName();
			nameIndex = spawnerComponent.nameIndex;
		}

		randomLvl = Random.Range(10, 101);
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


}