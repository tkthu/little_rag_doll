using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
	// Enemys bullets
	private List<GameObject> pooledStraightBullets;
	private int StraightBulletsNo = 15;
	private List<GameObject> pooledBounceBullets;
	private int BounceBulletsNo = 15;

	// Player bullets
	private List<GameObject> pooledPlayerStraightBullets;
	private int PlayerStraightBulletsNo = 15;
	private List<GameObject> pooledPlayerBounceBullets;
	private int PlayerBounceBulletsNo = 15;
	private List<GameObject> pooledPlayerExplodeBullets;
	private int PlayerExplodeBulletsNo = 5;
	private List<GameObject> pooledPlayerPistils;
	private int PlayerPistilsNo = 5;
	private List<GameObject> pooledPlayerBatterys;
	private int PlayerBatterysNo = 5;
	private List<GameObject> pooledPlayerBatteryDeads;
	private int PlayerBatteryDeadsNo = 5;


	public void instantiateAllPool()
	{
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
	}

	private List<GameObject> instantiatePool(string prefabName, int amountToPool, string parentObjectName)
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
		return getPooledObject(pooledStraightBullets, StraightBulletsNo);
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
	private GameObject getPooledObject(List<GameObject> pooledObject, int amountToPool)
	{
		for (int i = 0; i < amountToPool; i++)
		{
			if (!pooledObject[i].activeInHierarchy)
				return pooledObject[i];

		}
		return null;
	}

}
