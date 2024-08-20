using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	[SerializeField] GameObject imageBattale;

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	UnitP playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHudP playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	public GameObject hudBase;
	public GameObject hudInv;
	public GameObject hudChar;
	public GameObject hudConfirmChar;
	public GameObject hudAttack;

	public int index;

	private GameObject enemyGO;
	private GameObject playerGO;

	public GameObject attackImage;
	public GameObject _attackImage;
	private Spawner _spawner;

	private Ghostlymanager GhManager;
	private int ant;
	private Movement_Main movement;

	//Captura
	Dictionary<Capturaenum, int> _captura;
	public List<CapturaInfo> nameInfo;

	public List<StatsSave2> Statsinfo = new List<StatsSave2>(); 
	public int i = 0; //Captura randoms
	public int a = 0; //Botones de uso combate (especial, basico, escape y captura).
	public int b = 0; //Botones de abrir (inv y ataques).
	public int c = 0; //Botones de soporte (inv y ghostly cambio).
	void Start()
	{
		_captura = new Dictionary<Capturaenum, int>();
		for (int i = 0; i < nameInfo.Count; i++)
		{
			var curr = nameInfo[i];
			_captura[curr._chance] = curr.weight;
		}
		state = BattleState.START;
		_spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
		movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement_Main>();
	}

	private void Update()
    {
		if (enemyUnit != null)
		{
			enemyUnit.ChangeSprite(enemyUnit.nameIndex);
		}
	}

    public void SetUpC()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
	{
		movement.SaveStat();
		movement.velocidadMovimiento = 0;
		playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<UnitP>();

		enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "Un " + enemyUnit.unitName[enemyUnit.nameIndex] + " ha aparecido";

		playerHUD.SetHUDP(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	public IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(StatsManager.Instance._damage);

		enemyHUD.SetHP(enemyUnit.currentHP); //Vida enemy.
		attackImage.SetActive(true);
		AudioManager.instance.PlaySound(1);
		dialogueText.text = "Ataque exitoso!";
		
		StatsManager.Instance.stamina -=10;

		yield return new WaitForSeconds(0.5f);
		attackImage.SetActive(false);
		if(isDead)
		{
			state = BattleState.WON;
            StartCoroutine(EndBattle());
			yield return new WaitForSeconds(2f);
			imageBattale.SetActive(false);
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}
	public IEnumerator PlayerSPAttack()
	{
		bool isDead = enemyUnit.TakeDamage(StatsManager.Instance._damage+ (StatsManager.Instance._damage*10/100));

		enemyHUD.SetHP(enemyUnit.currentHP); //Vida enemy.
		attackImage.SetActive(true); //Futuro cambiar imagen de ataque a especial
		AudioManager.instance.PlaySound(1); //Futuro cambiar sonido de ataque especial.
		dialogueText.text = "Ataque especial exitoso!";

		StatsManager.Instance.stamina -= 80;

		yield return new WaitForSeconds(0.5f);
		attackImage.SetActive(false); //Cambiar imagen
		if (isDead)
		{
			state = BattleState.WON;
            StartCoroutine(EndBattle());
            yield return new WaitForSeconds(2f);
			imageBattale.SetActive(false);
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public IEnumerator EnemyTurn()
	{
		
		dialogueText.text = enemyUnit.unitName[enemyUnit.nameIndex] + " ataco!";

		yield return new WaitForSeconds(1f);
		_attackImage.SetActive(true);
		bool isDead = playerUnit.TakeDamage(enemyUnit.randomDamage); 
		AudioManager.instance.PlaySound(2);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);
		_attackImage.SetActive(false);
		if(isDead)
		{
			state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
		{
			state = BattleState.PLAYERTURN;
			StatsManager.Instance.stamina += 20;
			PlayerTurn();
		}
	}

    public IEnumerator EndBattle()
	{
		if(state == BattleState.WON)
		{

			AudioManager.instance.PlaySound(4);
			dialogueText.text = "¡Ganaste la batalla!";
            yield return new WaitForSeconds(1f);
            imageBattale.SetActive(false);
            hudAttack.SetActive(false);
            hudBase.SetActive(true);
            movement.velocidadMovimiento = movement.velmovsave;
            StatsManager.Instance._unitXp += Random.Range(200,500);
			GameManager.Instance.obv2 = true;
			StatsManager.Instance.Actualizar();

			if ((GameManager.Instance.pj) == 0)
			{
				StatsSave.Instance._xp1 = StatsManager.Instance._unitXp;
			}
			if ((GameManager.Instance.pj) == 1)
			{
				StatsSave.Instance._xp2 = StatsManager.Instance._unitXp;
			}
			if ((GameManager.Instance.pj) == 2)
			{
				StatsSave.Instance._xp3 = StatsManager.Instance._unitXp;
			}
			StatsManager.Instance.stamina = 100;
			Destroy(enemyGO);
			Destroy(playerGO,0.1f);
			_spawner.a = 0;
			_spawner.tiempoCombat = 0;
		} else if (state == BattleState.LOST)
		{
			
			AudioManager.instance.PlaySound(3);
			dialogueText.text = "¡Has perdido!.";
			yield return new WaitForSeconds(2f);
			GameManager.Instance.CharacterPassAway();
            Destroy(enemyGO);
			Destroy(playerGO);
			_spawner.a = 0;
		}
	}

	void PlayerTurn()
	{
		a = 0;
		b = 0;
		c = 0;
		dialogueText.text = "Elige una accion:";
	}

	public void PlayerInv()
	{
		GameManager.Instance.inv.SetActive(true);
		BackpackManager.Instance.ButtonItems();
	}

	public IEnumerator PlayerHeal(int amount)
	{
		playerUnit.Heal(amount);
		AudioManager.instance.PlaySound(7);
		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "¡Te has curado!";
		//GameManager.Instance.invActivo = !GameManager.Instance.invActivo;
		//GameManager.Instance.inv.SetActive(GameManager.Instance.invActivo);
		GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().showInventory();
		dialogueText.text = "¡Elige tu siguiente accion!";
		yield return new WaitForSeconds(1f);
	}

	public IEnumerator PlayerEscape()
	{
		movement.velocidadMovimiento = movement.velmovsave;
		dialogueText.text = "¡Te has escapado!";
		AudioManager.instance.PlaySound(6);
		yield return new WaitForSeconds(1f);
		imageBattale.SetActive(false);
		Destroy(enemyGO);
		Destroy(playerGO);
		_spawner.a = 0;
		_spawner.tiempoCombat = 0;
	}

	public IEnumerator InvEscape()
	{
		dialogueText.text = "¡Has abierto el inventario!";
		//AudioManager.instance.PlaySound(6);
		yield return new WaitForSeconds(1f);
		hudBase.SetActive(false);
		hudInv.SetActive(true);
	}
	public IEnumerator AttackSelect()
	{
		dialogueText.text = "¡Elije tu ataque!";
		//AudioManager.instance.PlaySound(6);
		yield return new WaitForSeconds(1f);
		hudBase.SetActive(false);
		hudAttack.SetActive(true);
	}

	public IEnumerator CharcSelect()
	{
		dialogueText.text = "¡Selecciona tu ghostly!";
		//AudioManager.instance.PlaySound(6);
		yield return new WaitForSeconds(1f);
		hudInv.SetActive(false);
		hudChar.SetActive(true);
	}

	public IEnumerator Back()
	{
		//AudioManager.instance.PlaySound(6);
		yield return new WaitForSeconds(1f);
		hudInv.SetActive(false);
		hudChar.SetActive(false);
		hudConfirmChar.SetActive(false);
		hudAttack.SetActive(false);
		hudBase.SetActive(true);
	}
	public int GetRandomChance()
	{
		var Capt = MyRandoms.Roulette(_captura); //Uso del My Random.
		return (int)Capt;
	}

	public IEnumerator PlayerCapture()
	{
		int chance = GetRandomChance();
		if (chance == 0)
		{
			movement.velocidadMovimiento = movement.velmovsave;
			dialogueText.text = "¡Captura en 3...2...1!";
			yield return new WaitForSeconds(2f);
			AudioManager.instance.PlaySound(4);
			dialogueText.text = "¡Captura exitosa!";
			GameManager.Instance.obv3 = true;
			GhController.Instance.colocarInv();
            if (i <= Statsinfo.Count)
            {
                Statsinfo[i]._damage = enemyUnit.randomDamage;
                Statsinfo[i]._lvl = enemyUnit.randomLvl;
                Statsinfo[i]._HPmax = enemyUnit.randomMaxHP;
                Statsinfo[i]._nameIndex = enemyUnit.nameIndex;
                i++;
            }
			GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventoryGh>().showInventory();
			yield return new WaitForSeconds(2f);
			Destroy(enemyGO);
			Destroy(playerGO);
			imageBattale.SetActive(false);
			hudInv.SetActive(false);
			hudBase.SetActive(true);
			_spawner.a = 0;
			_spawner.tiempoCombat = 0;
		}
		else
		{
			dialogueText.text = "¡Captura en 3...2...1!";
			yield return new WaitForSeconds(2f);
			dialogueText.text = "¡Captura no fue exitosa!";
			yield return new WaitForSeconds(2f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(Back());
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnAttackButton()
	{
		if (a == 0)
		{
			if(StatsManager.Instance.stamina >=0)
			{
				if (state != BattleState.PLAYERTURN)
				return;
				BackpackManager.Instance.fondoItems.SetActive(false);
				BackpackManager.Instance.slotsItems.SetActive(false);
				BackpackManager.Instance.slotsGh.SetActive(false);
				StartCoroutine(PlayerAttack());
				a = 1;
			}
			else
			dialogueText.text = "¡Stamina insuficiente!";
		}
	}

	public void OnSPAttackButton()
	{
		if (a == 0)
		{
			if (StatsManager.Instance.stamina >= 80)
			{
				if (state != BattleState.PLAYERTURN)
				return;
				BackpackManager.Instance.fondoItems.SetActive(false);
				BackpackManager.Instance.slotsItems.SetActive(false);
				BackpackManager.Instance.slotsGh.SetActive(false);
				StartCoroutine(PlayerSPAttack());
				a = 1;
			}
			else
			dialogueText.text = "¡Stamina insuficiente!";
		}
	}

	public void OnEscapeButton()
	{
		if (a == 0)
		{
			if (state != BattleState.PLAYERTURN)
				return;
			BackpackManager.Instance.fondoItems.SetActive(false);
			BackpackManager.Instance.slotsItems.SetActive(false);
			BackpackManager.Instance.slotsGh.SetActive(false);

			StartCoroutine(PlayerEscape());
			a = 1;
		}
	}
	public void OnOpenAttackButton()
	{
		if (b == 0)
		{
			if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(AttackSelect());
			b = 1;
		}
	}

	public void OnInventoryButton()
	{
		if (b == 0)
		{
			if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(InvEscape());
			b = 1;
		}
	}	

	public void OnBackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;
		BackpackManager.Instance.fondoItems.SetActive(false);
		BackpackManager.Instance.slotsItems.SetActive(false);
		BackpackManager.Instance.slotsGh.SetActive(false);

		StartCoroutine(Back());
		b = 0;
		c = 0;
	}

	public void OnInvButton()
	{
		if (c == 0)
		{
			if (state != BattleState.PLAYERTURN)
			return;

			BackpackManager.Instance.slotsGh.SetActive(false);
			PlayerInv();
			c = 1;
		}
	}

	public void OnCaptureButton()
	{
		if (a == 0)
		{
			if (state != BattleState.PLAYERTURN)
				return;
			BackpackManager.Instance.fondoItems.SetActive(false);
			BackpackManager.Instance.slotsItems.SetActive(false);
			BackpackManager.Instance.slotsGh.SetActive(false);

			StartCoroutine(PlayerCapture());
			a = 1;
		}
	}
	public void OnCharcButton()
	{
		if (c == 0)
		{
			if (state != BattleState.PLAYERTURN)
			return;
		BackpackManager.Instance.fondoItems.SetActive(false);
		BackpackManager.Instance.slotsItems.SetActive(false);
		BackpackManager.Instance.slotsGh.SetActive(false);

		StartCoroutine(CharcSelect());
			c = 1;
		}
	}
	//Boton para elegir personaje.
	public void ButtonCharc0()
	{
		ant = GameManager.Instance.pj;
		GameManager.Instance.pj = 0;
		hudConfirmChar.SetActive(true);
		StatsSave.Instance._xp3 = StatsManager.Instance._unitXp;
		StatsSave.Instance._xp2 = StatsManager.Instance._unitXp;
		StatsManager.Instance._unitXp = 0;
	}
	public void ButtonCharc1()
	{
		ant = GameManager.Instance.pj;
		GameManager.Instance.pj = 1;
		hudConfirmChar.SetActive(true);
		StatsSave.Instance._xp1 = StatsManager.Instance._unitXp;
		StatsSave.Instance._xp3 = StatsManager.Instance._unitXp;
		StatsManager.Instance._unitXp = 0;
	}
	public void ButtonCharc2()
	{
		ant = GameManager.Instance.pj;
		GameManager.Instance.pj = 2;
		hudConfirmChar.SetActive(true);
		StatsSave.Instance._xp1 = StatsManager.Instance._unitXp;
		StatsSave.Instance._xp2 = StatsManager.Instance._unitXp;
		StatsManager.Instance._unitXp = 0;
	}
	public void ButtonYes()
	{
		b = 0;
		c = 0;
		playerHUD.SetHUDP(playerUnit);
		playerUnit.currentHP=StatsManager.Instance._maxHP;
		StartCoroutine(Back());
	}
	public void ButtonNo()
	{
		b = 0;
		c = 0;
		GameManager.Instance.pj = ant;
		StartCoroutine(Back());
	}
}
