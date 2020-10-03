using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager GM;

	public KeyCode jump { get; set; }
	public KeyCode attack { get; set; }
	public KeyCode eatShoot { get; set; }
	public KeyCode map { get; set; }
	public KeyCode up { get; set; }
	public KeyCode down { get; set; }
	public KeyCode left { get; set; }
	public KeyCode right { get; set; }
	public int score { get; set; }

	public GameObject player;

	// Enemys bullets
	private List<GameObject> pooledStraightBullets;
	public int StraightBulletsNo = 15;	
	private List<GameObject> pooledBounceBullets;
	public int BounceBulletsNo = 15;

	// Player bullets
	private List<GameObject> pooledPlayerStraightBullets;
	public int PlayerStraightBulletsNo = 15;
	private List<GameObject> pooledPlayerBounceBullets;
	public int PlayerBounceBulletsNo = 15;
	private List<GameObject> pooledPlayerExplodeBullets;
	public int PlayerExplodeBulletsNo = 5;
	private List<GameObject> pooledPlayerPistils;
	public int PlayerPistilsNo = 5;
	private List<GameObject> pooledPlayerBatterys;
	public int PlayerBatterysNo = 5;
	private List<GameObject> pooledPlayerBatteryDeads;
	public int PlayerBatteryDeadsNo = 5;

	SceneLoader sceneLoader;

	void Awake()
	{
		//Singleton pattern
		if (GM == null)
		{
			DontDestroyOnLoad(gameObject);
			GM = this;
		}
		else if (GM != this)
		{
			Destroy(gameObject);
		}

		jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "K"));
		map = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("mapKey", "M"));
		eatShoot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("eatShootKey", "L"));
		attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey", "J"));
		up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey", "W"));
		down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S"));
		left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

		sceneLoader = GetComponent<SceneLoader>();
	}

	public void startGame()
    {
		player = Instantiate(player);
		player.SetActive(false);
		DontDestroyOnLoad(player);

		//khoi tao cac pool cho bullets cua quai (ko có đạn nổ)
		pooledStraightBullets = instantiatePool("StraightBullet", StraightBulletsNo, "StraightBullets");		
		pooledBounceBullets = instantiatePool("BounceBullet", BounceBulletsNo, "BounceBullets");
		//khoi tao cac pool cho bullets cua Player
		pooledPlayerStraightBullets = instantiatePool("StraightBullet_Player", PlayerStraightBulletsNo, "PlayerStraightBullets");
		pooledPlayerBounceBullets = instantiatePool("BounceBullet_Player", PlayerBounceBulletsNo, "PlayerBounceBullets");
		pooledPlayerExplodeBullets = instantiatePool("ExplodeBullet_Player", PlayerExplodeBulletsNo, "PlayerExplodeBullets");
		pooledPlayerPistils = instantiatePool("Pistil_Player", PlayerPistilsNo, "PlayerPistils");
		pooledPlayerBatterys = instantiatePool("Battery_Player", PlayerBatterysNo, "PlayerBatterys");
		pooledPlayerBatteryDeads = instantiatePool("Battery_dead_Player", PlayerBatteryDeadsNo, "PlayerBatteryDeads");

		score = 0;

	}
	public void loadScene(SceneName sn)
	{
		sceneLoader.loadScene(sn);
	}
	public void addScore(int amount)
	{
		score = score + amount;
	}

	#region POOLING BULLETS
	private List<GameObject> instantiatePool(string prefabName, int amountToPool,string parentObjectName)
    {
		GameObject parentObject = new GameObject(parentObjectName);
		List<GameObject> pooledObjects = new List<GameObject>();
		GameObject objectToPool = Resources.Load<GameObject>("Prefabs/Bullets/" + prefabName);
		GameObject tmp;
		for (int i = 0; i < amountToPool; i++)
        {
			tmp = Instantiate(objectToPool);
			tmp.SetActive(false);			
			tmp.transform.SetParent(parentObject.transform);
			pooledObjects.Add(tmp);
        }
		DontDestroyOnLoad(parentObject);
		return pooledObjects;
	}

	// lay dan dang inactive cua enemy
	public GameObject getStraightBullets()
	{
		return getPooledObject(pooledStraightBullets,StraightBulletsNo);
	}
	public GameObject getBounceBullets()
	{
		return getPooledObject(pooledBounceBullets, BounceBulletsNo);
	}

	// lay dan dang inactive cua Player
	public GameObject getPlayerStraightBullets()
	{
		return getPooledObject(pooledPlayerStraightBullets, PlayerStraightBulletsNo);
	}
	public GameObject getPlayerBounceBullets()
	{
		return getPooledObject(pooledPlayerBounceBullets, PlayerBounceBulletsNo);
	}
	public GameObject getPlayerExplodeBullets()
	{
		return getPooledObject(pooledPlayerExplodeBullets, PlayerExplodeBulletsNo);
	}
	public GameObject getPlayerPistils()
	{
		return getPooledObject(pooledPlayerPistils, PlayerPistilsNo);
	}
	public GameObject getPlayerBatterys()
	{
		return getPooledObject(pooledPlayerBatterys, PlayerBatterysNo);
	}
	public GameObject getPlayerBatteryDeads()
	{
		return getPooledObject(pooledPlayerBatteryDeads, PlayerBatteryDeadsNo);
	}
	private GameObject getPooledObject(List<GameObject> pooledObject,int amountToPool)
    {
		for(int i = 0; i < amountToPool; i++)
        {
			if (!pooledObject[i].activeInHierarchy)
				return pooledObject[i];

		}
		return null;
    }
	
	#endregion
		
}
