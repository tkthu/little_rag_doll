using System;
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

	[HideInInspector] public GameObject player;	

	private SceneLoader sceneLoader;
	[HideInInspector] public PoolingManager poolingManager;


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
		poolingManager = GetComponent<PoolingManager>();

	}


    public void startGame()
    {
		GameObject playerPrefabs = Resources.Load<GameObject>("Prefabs/Player/Player");
		player = Instantiate(playerPrefabs);
		player.SetActive(false);
		GameObject parentObject = new GameObject("GameObjects");
		player.transform.SetParent(parentObject.transform);
		DontDestroyOnLoad(parentObject);

		poolingManager.instantiateAllPool(parentObject);

		score = 0;

	}
	public void loadScene(SceneName sn)
	{
		poolingManager.inactiveAll();
		sceneLoader.loadScene(sn);
		
	}

    public void addScore(int amount)
	{
		score = score + amount;
	}

	
		
}
