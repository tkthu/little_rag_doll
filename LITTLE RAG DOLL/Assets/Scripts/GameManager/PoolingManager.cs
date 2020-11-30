using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
	// Enemys bullets
	private List<GameObject> pooledStraightBullets;
	private int StraightBulletsNo = 20;
	private List<GameObject> pooledBounceBullets;
	private int BounceBulletsNo = 20;

	// Player bullets
	private List<GameObject> pooledPlayerStraightBullets;
	private int PlayerStraightBulletsNo = 10;
	private List<GameObject> pooledPlayerBounceBullets;
	private int PlayerBounceBulletsNo = 10;

	// Enemies
	private List<GameObject> pooledBat;
	private int BatNo = 15;
	private List<GameObject> pooledSnail;
	private int SnailNo = 15;
	private List<GameObject> pooledFairy;
	private int FairyNo = 15;
	private List<GameObject> pooledBubbleBlower;
	private int BubbleBlowerNo = 15;

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
		listOfPool.Add(pooledPlayerStraightBullets);
		listOfPool.Add(pooledPlayerBounceBullets);

		//Khoi tao Enemies
		pooledBat = instantiatePool("Prefabs/Enemies/Bat", BatNo, "Bats");
		pooledSnail = instantiatePool("Prefabs/Enemies/Snail", SnailNo, "Snails");
		pooledFairy = instantiatePool("Prefabs/Enemies/Fairy", FairyNo, "Fairies");
		pooledBubbleBlower = instantiatePool("Prefabs/Enemies/BubbleBlower", BubbleBlowerNo, "BubbleBlowers");
		listOfPool.Add(pooledBat);
		listOfPool.Add(pooledSnail);
		listOfPool.Add(pooledFairy);
		listOfPool.Add(pooledBubbleBlower);

		listOfEnemisePool.Add(pooledBat);
		listOfEnemisePool.Add(pooledSnail);
		listOfEnemisePool.Add(pooledFairy);
		listOfEnemisePool.Add(pooledBubbleBlower);

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
