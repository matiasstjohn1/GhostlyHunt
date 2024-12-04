using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	[Header("				Variables del Script")]
	[SerializeField] GameObject imageBattale; //Escena del combate
	public BattleState state;
	[Header("Prefabs")]
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public GameObject enemyBossPrefab;
    public CustomEventExample _cust;

    [Header("Posiciones de batalla")]
	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	[Header("Scripts de unidades")]
	UnitP playerUnit;
	Unit enemyUnit;
	UnitBoss enemyBossUnit;
	StartCombat StartCombat;

	[Header("Canvas")]
	public Text dialogueText;
	public BattleHudP playerHUD;
	public BattleHUD enemyHUD;
	public GameObject hudBase;
	public GameObject hudInv;
	public GameObject hudChar;
	public GameObject hudChar1;
	public GameObject hudChar2;
	public GameObject hudChar3;
	public GameObject hudConfirmChar;
	public GameObject hudAttack;
	public GameObject attackImage; //efecto ataque player.
	public GameObject _attackImage;//efecto ataque normal enemigo.
	public GameObject _attackImage2; //efecto ataque especial enemigo.
	public GameObject LapidaPanel1;
	public GameObject LapidaPanel2;
	public GameObject onBackChar;
	public GameObject usePotion;

	[Header("Calls de turnos")]
	public int index;
	private GameObject enemyGO;
	private GameObject playerGO;

	[Header("Spawners del mundo")]
	public GameObject[] _spawner; //Spawners del mundo.

	private Ghostlymanager GhManager; //Llamo al GhostlyManager.
	private int ant;
	private Movement_Main movement; //Movimiento del player al estar en combate.
	Dictionary<Capturaenum, int> _captura;

	[Header("Probabilidad de captura")]
	public List<CapturaInfo> nameInfo;

	Dictionary<AtackEnum, int> _Ataque;
	[Header("Ataque enemigo")]
	public List<TipeAttack> ataqueInfo;

	[Header("Numericas de estadisticas")]
	public List<StatsSave2> Statsinfo = new List<StatsSave2>();
	public int i = 0; //Captura randoms
	public int a = 0; //Botones de uso combate (especial, basico, escape y captura).
	public int b = 0; //Botones de abrir (inv y ataques).
	public int c = 0; //Botones de soporte (inv y ghostly cambio).

	[Header("Jefe")]
	public GameObject[] Boss;
	GameObject player;
	public bool bossBattle = false;

	//Variables de habilidades//
	[Header("Habilidades")]
	private bool isShieldActive=false;
	private int d;
	private int corruptDamage;
	//Variables habilidades enemigo//
	private int _isHealing = -30;
	private int e;
	private int _corruptDamage;

	[Header("Var total de HP")]
	//Variable de Vida total en los 3 ghostlys, si llega a 0 muere//.
	public float totalHealth;

	void Start()
	{
		_captura = new Dictionary<Capturaenum, int>();
		for (int i = 0; i < nameInfo.Count; i++)
		{
			var curr = nameInfo[i];
			_captura[curr._chance] = curr.weight;
		}
		
		state = BattleState.START;
		_spawner = GameObject.FindGameObjectsWithTag("Spawner");
		movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement_Main>();

	}

    private void Update()
	{
        _cust.GhostlyTime += Time.deltaTime; 
        if (enemyUnit != null)
		{
			enemyUnit.ChangeSprite(enemyUnit.nameIndex);
		}
		//A los 2 turnos apaga habilidades//
		if(d >=2)
		{
			isShieldActive = false;
			corruptDamage = 0;
		}
		if (e >= 2)
		{
			_corruptDamage = 0;
		}
		_Ataque = new Dictionary<AtackEnum, int>();
		for (int i = 0; i < ataqueInfo.Count; i++)
		{
			var curre = ataqueInfo[i];
			_Ataque[curre._Chance] = curre.weight;
		}

		if (Boss != null)
		{
			// Encuentra todos los spawners y el jugador
			Boss = GameObject.FindGameObjectsWithTag("BossMap");
			player = GameObject.FindGameObjectWithTag("Player");

			GameObject closest = null;
			float closestDistance = Mathf.Infinity;

			//Itera sobre todos los spawners para encontrar el más cercano
			foreach (GameObject boss in Boss)
			{
				Vector3 bossPosition = boss.transform.position;
				Vector3 playerPosition = player.transform.position;
				Vector3 diff = bossPosition - playerPosition;
				float distance = diff.sqrMagnitude;

				//Compara la distancia para encontrar el spawner más cercano
				if (distance < closestDistance)
				{
					closest = boss;
					closestDistance = distance;
				}
			}
			try { 
			if(Boss.Length >=2)
			Boss[Boss.Length - 1] = Boss[0];
			Boss[0] = closest;
            }
			catch (System.IndexOutOfRangeException ex)
            {
				Debug.Log(ex);
				GameManager.Instance.obv5 = true;
			}
        }
	}
    private void FixedUpdate()
    {
		totalHealth = StatsSave.Instance.currentHealth1 + StatsSave.Instance.currentHealth2 + StatsSave.Instance.currentHealth3;
		if(StatsSave.Instance.currentHealth1 <=0)
		{
			StatsSave.Instance.currentHealth1 = 0;
		}
		if (StatsSave.Instance.currentHealth2 <= 0)
		{
			StatsSave.Instance.currentHealth2 = 0;
		}
		if (StatsSave.Instance.currentHealth3 <= 0)
		{
			StatsSave.Instance.currentHealth3 = 0;
		}

		if(hudChar.activeSelf)
			hudBase.SetActive(false);
	}
	public void SetUpC()
	{
		bossBattle = false;
		StartCoroutine(SetupBattle());
	}
	public void SetUpB(GameObject bossPrefab)
	{
		bossBattle = true;
		StartCoroutine(SetupBossBattle(bossPrefab));
	}

	public IEnumerator SetupBattle()
	{
		if (movement.velocidadMovimiento != 0)
		{
			movement.SaveStat();
		}
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
	//COMABATE CONTRA BOSS//
	public IEnumerator SetupBossBattle(GameObject bossPrefab)
	{
        if (movement.velocidadMovimiento != 0)
        {
            movement.SaveStat();
        }
        movement.velocidadMovimiento = 0;
		playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<UnitP>();

		enemyGO = Instantiate(bossPrefab, enemyBattleStation);
		enemyBossUnit = enemyGO.GetComponent<UnitBoss>();

		dialogueText.text = "Un " + enemyBossUnit.unitName + " ha aparecido";

		playerHUD.SetHUDP(playerUnit);
		enemyHUD.SetBossHUD(enemyBossUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	public IEnumerator PlayerAttack()
	{
		bool isDead = false;
		if (enemyUnit!=null)
		{
			isDead = enemyUnit.TakeDamage(StatsManager.Instance._damage+corruptDamage);
			enemyHUD.SetHP(enemyUnit.currentHP); //Vida enemy.
			attackImage.SetActive(true);
			AudioManager.instance.PlayCombatSounds(1);
			dialogueText.text = "Ataque exitoso!";

			StatsManager.Instance.stamina -= 10;
		}
		if (bossBattle==true)
		{
			isDead = enemyBossUnit.TakeDamage(StatsManager.Instance._damage + corruptDamage);
			enemyHUD.SetHP(enemyBossUnit.currentHP); //Vida enemy.
			attackImage.SetActive(true);
			AudioManager.instance.PlayCombatSounds(1);
			dialogueText.text = "Ataque exitoso!";

			StatsManager.Instance.stamina -= 10;
		}

		yield return new WaitForSeconds(0.5f);
		attackImage.SetActive(false);
		if (isDead)
		{
			state = BattleState.WON;
			StartCoroutine(EndBattle());
			if(enemyBossUnit!=null) 
			{
				Destroy(Boss[0]);
				GameManager.Instance.bossCount += 1;
				GameManager.Instance.ActualizarObjetivo5();
			}
			yield return new WaitForSeconds(2f);
			imageBattale.SetActive(false);

		}
		else
		{
			if (enemyUnit != null)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyTurn());
			}
			if (bossBattle==true)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyBossTurn());
			}
		}
	}
	public IEnumerator PlayerSPAttack()
	{
		bool isDead = false;

		if (enemyUnit != null)
		{
		    isDead = enemyUnit.TakeDamage(StatsManager.Instance._damage + (StatsManager.Instance._damage * 60 / 100) + corruptDamage);
			enemyHUD.SetHP(enemyUnit.currentHP); //Vida enemy.
			attackImage.SetActive(true); //Futuro cambiar imagen de ataque a especial
			AudioManager.instance.PlayCombatSounds(1); //Futuro cambiar sonido de ataque especial.
			dialogueText.text = "Ataque especial exitoso!";
			StatsManager.Instance.stamina -= 80;
		}

		if (bossBattle == true)
		{
			isDead = enemyBossUnit.TakeDamage(StatsManager.Instance._damage + (StatsManager.Instance._damage * 60 / 100) + corruptDamage);
			enemyHUD.SetHP(enemyBossUnit.currentHP); //Vida enemy.
			attackImage.SetActive(true); //Futuro cambiar imagen de ataque a especial
			AudioManager.instance.PlayCombatSounds(1); //Futuro cambiar sonido de ataque especial.
			dialogueText.text = "Ataque especial exitoso!";
			StatsManager.Instance.stamina -= 80;
		}

		yield return new WaitForSeconds(0.5f);
		attackImage.SetActive(false); //Cambiar imagen
		if (isDead)
		{
			state = BattleState.WON;
			StartCoroutine(EndBattle());
			if (enemyBossUnit != null) 
			{
				Destroy(Boss[0]);
				GameManager.Instance.bossCount += 1;
				GameManager.Instance.ActualizarObjetivo5();
				if (GameManager.Instance.bossCount >= 1)
                {
                    _cust.ObjectiveIDa = 1005;
                    _cust.OnLevelComplete();
                }
			}
			yield return new WaitForSeconds(2f);
			imageBattale.SetActive(false);
		}
		else
		{
			if(enemyUnit != null)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyTurn());
			}
			if (bossBattle == true)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyBossTurn());
			}
		}
	}

	public IEnumerator DefinitiveAttack()
	{
		bool isDead = false;
		//Escudo habilidad si son ghostlys 0 al 4//
		if (enemyUnit != null&&(StatsManager.Instance._index == 0 || StatsManager.Instance._index == 1 || StatsManager.Instance._index == 2 || StatsManager.Instance._index == 3 || StatsManager.Instance._index == 4 || StatsManager.Instance._index == 5 || StatsManager.Instance._index == 6 || StatsManager.Instance._index == 7))
		{
			isShieldActive = true;
			//shieldImage.SetActive(true); IMAGEN HABILIDAD ESCUDO
			//AudioManager.instance.PlayCombatSounds(1); AUDIO ESCUDO
			dialogueText.text = "Escudo Activado!";
			StatsManager.Instance.stamina -= 120;
			d = 0;
		}
		//Corrupcion habilidad si son ghostlys 5 al 9 suma daño//
		if (enemyUnit != null && (StatsManager.Instance._index == 8 || StatsManager.Instance._index == 9 || StatsManager.Instance._index == 12 || StatsManager.Instance._index == 13 || StatsManager.Instance._index == 16))
		{
			corruptDamage = 80;
			//corruptImage.SetActive(true); IMAGEN HABILIDAD CORRUPT
			//AudioManager.instance.PlayCombatSounds(1); AUDIO CORRUPT
			dialogueText.text = "Corrupcion Activada!";
			StatsManager.Instance.stamina -= 120;
			d = 0;
		}
		//Autoexplocion habilidad si son ghostlys 10 al 14 suma daño//
		if (enemyUnit != null && (StatsManager.Instance._index == 10 || StatsManager.Instance._index == 11 || StatsManager.Instance._index == 14 || StatsManager.Instance._index == 15 || StatsManager.Instance._index == 17 || StatsManager.Instance._index == 18 || StatsManager.Instance._index == 19))
		{
			isDead = enemyUnit.TakeDamage(10000);
			playerUnit.TakeDamage((int)playerUnit.currentHP - 1);
			//destroyImage.SetActive(true); IMAGEN HABILIDAD AUTOEXPLOCION
			//AudioManager.instance.PlayCombatSounds(1); AUDIO EXPLOCION
			dialogueText.text = "Autoexploción Activado!";
			StatsManager.Instance.stamina -= 120;
		}


		//BOSS COMBAT HABILIDAD//
		//Escudo habilidad si son ghostlys 0 al 4//
		if (bossBattle == true && (StatsManager.Instance._index == 0 || StatsManager.Instance._index == 1 || StatsManager.Instance._index == 2 || StatsManager.Instance._index == 3 || StatsManager.Instance._index == 4))
		{
			isShieldActive = true;
			//shieldImage.SetActive(true); IMAGEN HABILIDAD ESCUDO
			//AudioManager.instance.PlayCombatSounds(1); AUDIO ESCUDO
			dialogueText.text = "Escudo Activado!";
			StatsManager.Instance.stamina -= 120;
			d = 0;
		}
		//Corrupcion habilidad si son ghostlys 5 al 9 suma daño//
		if (bossBattle == true && (StatsManager.Instance._index == 5 || StatsManager.Instance._index == 6 || StatsManager.Instance._index == 7 || StatsManager.Instance._index == 8 || StatsManager.Instance._index == 9))
		{
			corruptDamage = 80;
			//corruptImage.SetActive(true); IMAGEN HABILIDAD CORRUPT
			//AudioManager.instance.PlayCombatSounds(1); AUDIO CORRUPT
			dialogueText.text = "Corrupcion Activada!";
			StatsManager.Instance.stamina -= 120;
			d = 0;
		}
		//Autoexplocion habilidad si son ghostlys 10 al 14 suma daño//
		if (bossBattle == true && (StatsManager.Instance._index == 10 || StatsManager.Instance._index == 11 || StatsManager.Instance._index == 12 || StatsManager.Instance._index == 13 || StatsManager.Instance._index == 14))
		{
			isDead = enemyBossUnit.TakeDamage(200);
			playerUnit.TakeDamage(100);
			enemyHUD.SetHP(enemyBossUnit.currentHP); //Vida enemy.
			//destroyImage.SetActive(true); IMAGEN HABILIDAD AUTOEXPLOCION
			//AudioManager.instance.PlayCombatSounds(1); AUDIO EXPLOCION
			dialogueText.text = "Autoexploción Activado!";
			StatsManager.Instance.stamina -= 120;
		}

		yield return new WaitForSeconds(0.5f);
		//shieldImage.SetActive(false); IMAGEN HABILIDAD ESCUDO
		if (isDead)
		{
			state = BattleState.WON;
			StartCoroutine(EndBattle());
			if (enemyBossUnit != null)
			{
				Destroy(Boss[0]);
				GameManager.Instance.bossCount += 1;
				GameManager.Instance.ActualizarObjetivo5();
			}
			yield return new WaitForSeconds(2f);
			imageBattale.SetActive(false);
		}
		else
		{
			if (enemyUnit != null)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyTurn());
			}
			if (bossBattle == true)
			{
				state = BattleState.ENEMYTURN;
				StartCoroutine(EnemyBossTurn());
			}
		}
	}

	public int GetTypeChance()
	{
		var type = MyRandoms.Roulette(_Ataque); //Uso del My Random.
		return (int)type;
	}
	public IEnumerator EnemyTurn()
	{
		int chance = GetTypeChance();
		bool isDead = false;

		yield return new WaitForSeconds(1f);
		if (!isShieldActive)
		{
			//Especial
			if (chance == 0)
			{
				Debug.Log("Ataque SP");
				_attackImage2.SetActive(true);
				dialogueText.text = enemyUnit.unitName[enemyUnit.nameIndex] + " uso ataque especial!";
				isDead = playerUnit.TakeDamage((int)enemyUnit.randomDamage + (int)(enemyUnit.randomDamage * 0.3f)+_corruptDamage);
				AudioManager.instance.PlayCombatSounds(0);
				yield return new WaitForSeconds(1f);
				_attackImage2.SetActive(false);
			}
			//Basico.
			if (chance == 1)
			{
				Debug.Log("Ataque Basic");
				_attackImage.SetActive(true);
				dialogueText.text = enemyUnit.unitName[enemyUnit.nameIndex] + " ataco!";
				isDead = playerUnit.TakeDamage((int)enemyUnit.randomDamage+_corruptDamage);
				AudioManager.instance.PlayCombatSounds(0);
				yield return new WaitForSeconds(1f);
				_attackImage.SetActive(false);
			}
			//Curacion.
			if (chance == 2)
			{
				Debug.Log("Curacion");
				isDead = enemyUnit.TakeDamage(_isHealing);
				//_shieldImage.SetActive(true);
				dialogueText.text = enemyUnit.unitName[enemyUnit.nameIndex] + " uso curación!";
				AudioManager.instance.PlayCombatSounds(0);
				//_shieldImage.SetActive(false);
				e = 0;
				yield return new WaitForSeconds(1f);
			}
			//Corrupcion.
			if (chance == 3)
			{
				Debug.Log("Corrupcion");
				//_corruptImage.SetActive(true);
				_corruptDamage = 80;
				dialogueText.text = enemyUnit.unitName[enemyUnit.nameIndex] + " uso corrupcion!";
				AudioManager.instance.PlayCombatSounds(0);
				//_corruptImage.SetActive(false);
				e = 0;
				yield return new WaitForSeconds(1f);
			}
			//Autodestruccion.
			if (chance == 4)
			{
				Debug.Log("Autodesruccion");
				//_destroyImage.SetActive(true);
				dialogueText.text = enemyUnit.unitName[enemyUnit.nameIndex] + " se autodestruyo!";
				isDead = enemyUnit.TakeDamage(10000);
				isDead = playerUnit.TakeDamage((int)playerUnit.currentHP+10);
				AudioManager.instance.PlayCombatSounds(0);
				//_destroyImage.SetActive(false);
				yield return new WaitForSeconds(1f);
			}
		}
		if (totalHealth <= 0)
		{
			state = BattleState.LOST;
			StartCoroutine(EndBattle());
		}
		else
		{
			state = BattleState.PLAYERTURN;
			StatsManager.Instance.stamina += 20;
			PlayerTurn();
		}
	}
	//Turno del Boss//
	public IEnumerator EnemyBossTurn()
	{
		int chance = GetTypeChance();
		dialogueText.text = enemyBossUnit.unitName + " ataco!";
		bool isDead = false;

		yield return new WaitForSeconds(1f);

		if(!isShieldActive)
		{
			if (chance == 0)
			{
				_attackImage2.SetActive(true);
				isDead = playerUnit.TakeDamage((int)enemyBossUnit.Damage + (int)(enemyBossUnit.Damage * 0.3f));
				AudioManager.instance.PlayCombatSounds(0);

				playerHUD.SetHP(playerUnit.currentHP);

				yield return new WaitForSeconds(1f);
				_attackImage2.SetActive(false);
			}
			if (chance == 1)
			{
				_attackImage.SetActive(true);
				isDead = playerUnit.TakeDamage((int)enemyBossUnit.Damage);
				AudioManager.instance.PlayCombatSounds(0);

				playerHUD.SetHP(playerUnit.currentHP);

				yield return new WaitForSeconds(1f);
				_attackImage.SetActive(false);
			}
	    }
		if (totalHealth <= 0)
		{
			state = BattleState.LOST;
			StartCoroutine(EndBattle());
		}
		else
		{
			state = BattleState.PLAYERTURN;
			StatsManager.Instance.stamina += 20;
			PlayerTurn();
		}
	}

	public IEnumerator EndBattle()
	{
		if (state == BattleState.WON)
		{
            _cust.ObjectiveIDa = 1002;
            _cust.OnLevelComplete();
            AudioManager.instance.PlayCombatSounds(6);
			dialogueText.text = "¡Ganaste la batalla!";
			yield return new WaitForSeconds(1f);

			foreach (GameObject _spawner in _spawner)
			{
				_spawner.GetComponent<Spawner>().a = 0;
				_spawner.GetComponent<Spawner>().tiempoCombat = 0;
			}

			movement.velocidadMovimiento = movement.velmovsave;
			StatsManager.Instance._unitXp += Random.Range(200, 500);
			GameManager.Instance.obv2 = true;
			StatsManager.Instance.Actualizar();

			if ((GameManager.Instance.pj) == 0)
			{
				StatsSave.Instance._xp1 = StatsManager.Instance._unitXp;
				StatsSave.Instance.currentHealth1 = StatsManager.Instance.currentHealth;
			}
			if ((GameManager.Instance.pj) == 1)
			{
				StatsSave.Instance._xp2 = StatsManager.Instance._unitXp;
				StatsSave.Instance.currentHealth2 = StatsManager.Instance.currentHealth;
			}
			if ((GameManager.Instance.pj) == 2)
			{
				StatsSave.Instance._xp3 = StatsManager.Instance._unitXp;
				StatsSave.Instance.currentHealth3 = StatsManager.Instance.currentHealth;
			}
			StatsManager.Instance.stamina = 100;
			Destroy(enemyGO);
			Destroy(playerGO, 0.1f);
            imageBattale.SetActive(false);
            hudAttack.SetActive(false);
            hudBase.SetActive(true);

        }
		else if (state == BattleState.LOST)
		{

			AudioManager.instance.PlayCombatSounds(5);
			dialogueText.text = "¡Has perdido!.";
			yield return new WaitForSeconds(2f);
			AudioManager.instance.StopSounds();
			
			Destroy(enemyGO);
			Destroy(playerGO);
			foreach (GameObject _spawner in _spawner)
			{
				_spawner.GetComponent<Spawner>().a = 0;
				_spawner.GetComponent<Spawner>().tiempoCombat = 0;
			}
			GameManager.Instance.CharacterPassAway();
		}
	}

	void PlayerTurn()
	{
		a = 0;
		b = 0;
		c = 0;
		d += 1;
		e += 1;
		dialogueText.text = "Elige una accion:";
		//Sino esta esto no oculta los botones de los ghostlys que murieron//
		if (StatsManager.Instance.currentHealth <= 0)
		{
			hudAttack.SetActive(false);
			hudChar.SetActive(true);
		}
		if (StatsSave.Instance.currentHealth1 <= 0)
		{
			onBackChar.SetActive(false);
			hudChar1.SetActive(false);
		}
		if (StatsSave.Instance.currentHealth2 <= 0)
		{
			onBackChar.SetActive(false);
			hudChar2.SetActive(false);
		}
		if (StatsSave.Instance.currentHealth3 <= 0)
		{
			onBackChar.SetActive(false);
			hudChar3.SetActive(false);
		}
		//Sino esta esto no se ven los botones//
		if (StatsSave.Instance.currentHealth1 > 0)
		{
			hudChar1.SetActive(true);
		}
		if (StatsSave.Instance.currentHealth2 > 0)
		{
			hudChar2.SetActive(true);
		}
		if (StatsSave.Instance.currentHealth3 > 0)
		{
			hudChar3.SetActive(true);
		}
	}

	public void PlayerInv()
	{
		GameManager.Instance.inv.SetActive(true);
		BackpackManager.Instance.ButtonItems();
	}

	public IEnumerator PlayerHeal(int amount)
	{
		if (playerUnit != null)
		{
			playerUnit.Heal(amount);
			AudioManager.instance.PlayCombatSounds(3);
			playerHUD.SetHP(playerUnit.currentHP);
			dialogueText.text = "¡Te has curado!";
			GameObject.FindGameObjectWithTag("InventarioM").GetComponent<InventroyManager>().showInventory();
			dialogueText.text = "¡Elige tu siguiente accion!";
			yield return new WaitForSeconds(1f);
		}
		if (playerUnit == null)
		{
			usePotion.SetActive(true);
            StartCoroutine(Deactivatetext());

        } 

	}
	public IEnumerator Deactivatetext()
	{
        yield return new WaitForSeconds(3f);
        usePotion.SetActive(false);
    }

    public IEnumerator PlayerEscape()
	{
		foreach (GameObject _spawner in _spawner)
		{
			_spawner.GetComponent<Spawner>().a = 0;
			_spawner.GetComponent<Spawner>().tiempoCombat = 0;
		}
		movement.velocidadMovimiento = movement.velmovsave;
		dialogueText.text = "¡Te has escapado!";
		AudioManager.instance.PlayCombatSounds(2);
		yield return new WaitForSeconds(1f);
		imageBattale.SetActive(false);
		Destroy(enemyGO);
		Destroy(playerGO);

	}

	public IEnumerator InvEscape()
	{
		dialogueText.text = "¡Has abierto el inventario!";
		yield return new WaitForSeconds(1f);
		hudBase.SetActive(false);
		hudInv.SetActive(true);
	}
	public IEnumerator AttackSelect()
	{
		dialogueText.text = "¡Elije tu ataque!";
		yield return new WaitForSeconds(1f);
		hudBase.SetActive(false);
		hudAttack.SetActive(true);
	}

	public IEnumerator CharcSelect()
	{
		dialogueText.text = "¡Selecciona tu ghostly!";
		yield return new WaitForSeconds(1f);
		hudConfirmChar.SetActive(false);
		hudInv.SetActive(false);
		hudChar.SetActive(true);
	}

	public IEnumerator Back()
	{
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

		if (chance == 0 && bossBattle == false)
		{
            _cust.ObjectiveIDa = 1003;
            _cust.GhostlyIDa = enemyUnit.nameIndex;
            _cust.OnLevelComplete();

            dialogueText.text = "¡Captura en 3...2...1!";
			yield return new WaitForSeconds(2f);
			AudioManager.instance.PlayCombatSounds(6);
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
            foreach (GameObject _spawner in _spawner)
            {
                _spawner.GetComponent<Spawner>().a = 0;
                _spawner.GetComponent<Spawner>().tiempoCombat = 0;
            } 
            movement.velocidadMovimiento = movement.velmovsave;
            imageBattale.SetActive(false);
			hudInv.SetActive(false);
			hudBase.SetActive(true);


		}
		if (chance == 1&&bossBattle==false)
		{
			dialogueText.text = "¡Captura en 3...2...1!";
			yield return new WaitForSeconds(2f);
			dialogueText.text = "¡Captura no fue exitosa!";
			yield return new WaitForSeconds(2f);
			state = BattleState.ENEMYTURN;
			StartCoroutine(Back());
			StartCoroutine(EnemyTurn());
		}
		if (bossBattle == true) 
		{
            dialogueText.text = "¡No puedes capturarlo!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(Back());
            StartCoroutine(EnemyBossTurn());
        }
    }
	//SECCION DE BOTONES EN COMBATE//
	//Boton de ataque basico//
	public void OnAttackButton()
	{
		if (a == 0)
		{
			if (StatsManager.Instance.stamina >= 0)
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
	//Boton de ataque especial//
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
	//Boton de ataque definitivo//
	public void OnDefinitiveButton()
	{
		if (a == 0)
		{
			if (StatsManager.Instance.stamina >= 120)
			{
				if (state != BattleState.PLAYERTURN)
					return;
				BackpackManager.Instance.fondoItems.SetActive(false);
				BackpackManager.Instance.slotsItems.SetActive(false);
				BackpackManager.Instance.slotsGh.SetActive(false);
				StartCoroutine(DefinitiveAttack());
				a = 1;
			}
			else
				dialogueText.text = "¡Stamina insuficiente!";
		}
	}
	//Boton de escape//
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
	//Boton de elegir tipo de ataque//
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
	//Boton de inventario//
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
	//Boton de atras//
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
	//Boton de inventario seccion//
	public void OnInvButton()
	{
		if (c == 0)
		{
			a = 1;
			if (state != BattleState.PLAYERTURN)
				return;

			BackpackManager.Instance.slotsGh.SetActive(false);
			PlayerInv();
			c = 1;
		}
	}
	//Boton de captura//
	public void OnCaptureButton()
	{
		if (a == 0)
		{
			c = 1;
			if (state != BattleState.PLAYERTURN)
				return;
			BackpackManager.Instance.fondoItems.SetActive(false);
			BackpackManager.Instance.slotsItems.SetActive(false);
			BackpackManager.Instance.slotsGh.SetActive(false);

			StartCoroutine(PlayerCapture());
			a = 1;
		}
	}
	//Boton de elegir ghostly//
	public void OnCharcButton()
	{
		if (c == 0)
		{
			a = 1;
			onBackChar.SetActive(true);
			if (state != BattleState.PLAYERTURN)
				return;
			BackpackManager.Instance.fondoItems.SetActive(false);
			BackpackManager.Instance.slotsItems.SetActive(false);
			BackpackManager.Instance.slotsGh.SetActive(false);

			if (StatsSave.Instance.currentHealth1 > 0)
			{
				hudChar1.SetActive(true);
			}
			if (StatsSave.Instance.currentHealth2 > 0)
			{
				hudChar2.SetActive(true);
			}
			if (StatsSave.Instance.currentHealth3 > 0)
			{
				hudChar3.SetActive(true);
			}

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
        
		_cust.GhostlyTypec = playerUnit.unitName[StatsManager.Instance._index];
		_cust.OnGhostlyEquipedOnCombat();
		_cust.GhostlyTime = 0;


        if ((GameManager.Instance.pj) == 0)
		{
			playerUnit.currentHP = StatsSave.Instance.currentHealth1;
		}
		if ((GameManager.Instance.pj) == 1)
		{
			playerUnit.currentHP = StatsSave.Instance.currentHealth2;
		}
		if ((GameManager.Instance.pj) == 2)
		{
			playerUnit.currentHP = StatsSave.Instance.currentHealth3;
		}
		playerHUD.SetHUDP(playerUnit);
		StartCoroutine(Back());
	}
	public void ButtonNo()
	{
		b = 0;
		c = 0;
		GameManager.Instance.pj = ant;
		StartCoroutine(CharcSelect());
	}
}

