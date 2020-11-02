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

	// Enemies
	private List<GameObject> pooledBat;
	private int BatNo = 15;
	private List<GameObject> pooledSnail;
	private int SnailNo = 15;
	private List<GameObject> pooledFairy;
	private int FairyNo = 15;
	private List<GameObject> pooledBubbleBlower;
	private int BubbleBlowerNo = 15;
	private List<GameObject> pooledFrog;
	private int FrogNo = 5;

	// Interactable
	private List<GameObject> pooledFlower;
	private int FlowerNo = 5;
	private List<GameObject> pooledHelmet;
	private int HelmetNo = 5;

	private List<List<GameObject>> listOfPool = new List<List<GameObject>>();
	private List<List<GameObject>> listOfEnemisePool = new List<List<GameObject>>();
	private List<List<GameObject>> listOfInteractablesPool = new List<List<GameObject>>();

	private GameObject parentObject;

	public void inactiveAll()
    {
		foreach(List<GameObject> pooled in listOfPool)
			foreach (GameObject go in pooled)
				go.GetComponent<PoolingItem>().resetState();
		
	}

    // Khoi tao Pool
    #region Khoi Tao
    public void instantiateAllPool(GameObject parentObject)
	{
		this.parentObject = parentObject;
		//khoi tao cac pool cho bullets cua quai (ko có đạn nổ)
		pooledStraightBullets = instantiatePool("Prefabs/Bullets/StraightBullet", StraightBulletsNo, "StraightBullets");
		pooledBounceBullets = instantiatePool("Prefabs/Bullets/BounceBullet", BounceBulletsNo, "BounceBullets");
		listOfPool.Add(pooledStraightBullets);
		listOfPool.Add(pooledBounceBullets);

		//khoi tao cac pool cho bullets cua Player
		pooledPlayerStraightBullets = instantiatePool("Prefabs/Bullets/StraightBullet_Player", PlayerStraightBulletsNo, "PlayerStraightBullets");
		pooledPlayerBounceBullets = instantiatePool("Prefabs/Bullets/BounceBullet_Player", PlayerBounceBulletsNo, "PlayerBounceBullets");
		pooledPlayerExplodeBullets = instantiatePool("Prefabs/Bullets/ExplodeBullet_Player", PlayerExplodeBulletsNo, "PlayerExplodeBullets");
		pooledPlayerPistils = instantiatePool("Prefabs/Bullets/Pistil_Player", PlayerPistilsNo, "PlayerPistils");
		pooledPlayerBatterys = instantiatePool("Prefabs/Bullets/Battery_Player", PlayerBatterysNo, "PlayerBatterys");
		pooledPlayerBatteryDeads = instantiatePool("Prefabs/Bullets/Battery_dead_Player", PlayerBatteryDeadsNo, "PlayerBatteryDeads");
		listOfPool.Add(pooledPlayerStraightBullets);
		listOfPool.Add(pooledPlayerBounceBullets);
		listOfPool.Add(pooledPlayerExplodeBullets);
		listOfPool.Add(pooledPlayerPistils);
		listOfPool.Add(pooledPlayerBatterys);
		listOfPool.Add(pooledPlayerBatteryDeads);

		//Khoi tao Enemies
		pooledBat = instantiatePool("Prefabs/Enemies/Bat", BatNo, "Bats");
		pooledSnail = instantiatePool("Prefabs/Enemies/Snail", SnailNo, "Snails");
		pooledFairy = instantiatePool("Prefabs/Enemies/Fairy", FairyNo, "Fairies");
		pooledBubbleBlower = instantiatePool("Prefabs/Enemies/BubbleBlower", BubbleBlowerNo, "BubbleBlowers");
		pooledFrog = instantiatePool("Prefabs/Bullets/ExplodeBullet", FrogNo, "Frogs");
		listOfPool.Add(pooledBat);
		listOfPool.Add(pooledSnail);
		listOfPool.Add(pooledFairy);
		listOfPool.Add(pooledBubbleBlower);
		listOfPool.Add(pooledFrog);

		listOfEnemisePool.Add(pooledBat);
		listOfEnemisePool.Add(pooledSnail);
		listOfEnemisePool.Add(pooledFairy);
		listOfEnemisePool.Add(pooledBubbleBlower);
		listOfEnemisePool.Add(pooledFrog);

		//Khoi tao Interactable
		pooledFlower = instantiatePool("Prefabs/Interactables/Flower", FlowerNo, "Flowers");
		pooledHelmet = instantiatePool("Prefabs/Interactables/Helmet", HelmetNo, "Helmets");
		listOfPool.Add(pooledFlower);
		listOfPool.Add(pooledHelmet);

		listOfInteractablesPool.Add(pooledFlower);
		listOfInteractablesPool.Add(pooledHelmet);
	}
	private List<GameObject> instantiatePool(string prefabName, int amountToPool, string parentObjectName)
	{
		GameObject groupObject = new GameObject(parentObjectName);
		List<GameObject> pooledObjects = new List<GameObject>();
		GameObject objectToPool = Resources.Load<GameObject>( prefabName);
		GameObject tmp;
		for (int i = 0; i < amountToPool; i++)
		{
			tmp = Instantiate(objectToPool);
			tmp.SetActive(false);
			tmp.AddComponent<PoolingItem>().setOriginalParent(groupObject.transform);
			pooledObjects.Add(tmp);
		}
		groupObject.transform.SetParent(parentObject.transform);
		return pooledObjects;
	}
    #endregion

    // lay dan dang inactive cua enemy
    #region Enemy Bullets
    public GameObject getStraightBullets()
	{
		return getPooledObject(pooledStraightBullets, StraightBulletsNo);
	}
	public GameObject getBounceBullets()
	{
		return getPooledObject(pooledBounceBullets, BounceBulletsNo);
	}
	#endregion

	// lay dan dang inactive cua Player
	#region Player Bullets
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
	#endregion

	// lay enemy inactive
	#region Enemies
	public GameObject getBat()
	{
		return getPooledObject(pooledBat, BatNo);
	}
	public GameObject getSnail()
	{
		return getPooledObject(pooledSnail, SnailNo);
	}
	public GameObject getFairy()
	{
		return getPooledObject(pooledFairy, FairyNo);
	}
	public GameObject getBubbleBlower()
	{
		return getPooledObject(pooledBubbleBlower, BubbleBlowerNo);
	}
	public GameObject getFrog()
	{
		return getPooledObject(pooledFrog, FrogNo);
	}
	#endregion

	// lay Interactables inactive
	#region Interactables
	public GameObject getFlower()
	{
		return getPooledObject(pooledFlower, FlowerNo);
	}
	public GameObject getHelmet()
	{
		return getPooledObject(pooledHelmet, HelmetNo);
	}
    #endregion

    private GameObject getPooledObject(List<GameObject> pooledObject, int amountToPool)
	{
		for (int i = 0; i < amountToPool; i++)
			if (!pooledObject[i].activeInHierarchy)
				return pooledObject[i];
		return null;
	}

	// lay listOfPool
	public List<List<GameObject>> getlistOfEnemiesPool()
    {
		return listOfEnemisePool;

	}
	public List<List<GameObject>> getListOfInteractablesPool()
	{
		return listOfInteractablesPool;

	}
}
