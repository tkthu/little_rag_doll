﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager GM;

	public KeyCode jump { get; set; }
    public KeyCode attack { get; set; }
	public KeyCode eatShoot { get; set; }
	public KeyCode interact { get; set; }
	public KeyCode up { get; set; }
	public KeyCode down { get; set; }
	public KeyCode left { get; set; }
	public KeyCode right { get; set; }
	public int score { get; set; }

	[HideInInspector] public GameObject player;	

	private SceneLoader sceneLoader;
	[HideInInspector] public GameTimer gameTimer;
	[HideInInspector] public PoolingManager poolingManager;
	[HideInInspector] public MusicManager musicManager;

	private Text scoreSpirit;

	[HideInInspector] public bool isGameover = false;
	[HideInInspector] public bool loadAtCheckpoint = false;
	[HideInInspector] public bool GameIsPaused = false;
	[HideInInspector] public GameObject GameUI;
	[HideInInspector] public GameObject PauseMenu;
	[HideInInspector] public GameObject GameOverMenu;
	[HideInInspector] public GameObject audioManager;

	private bool firstTime = true;

	private GameData gameData;

	[HideInInspector] public List<GameObject> collected;
	[HideInInspector] public Dictionary<string,SceneData> tempSavedSceneData;

	[HideInInspector] public bool testing = false;

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
		eatShoot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("eatShootKey", "L"));
		attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey", "J"));
		up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey", "W"));
		down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S"));
		left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

		sceneLoader = GetComponent<SceneLoader>();
		poolingManager = GetComponent<PoolingManager>();
		gameTimer = GetComponent<GameTimer>();
		musicManager = transform.Find("Audio Manager").GetComponent<MusicManager>();

		GameUI = transform.Find("GameUI").gameObject;
		PauseMenu = transform.Find("PauseMenu").gameObject;
		GameOverMenu = transform.Find("GameOverMenu").gameObject;
		audioManager = transform.Find("Audio Manager").gameObject;

		scoreSpirit = GameUI.transform.Find("SpiritText").gameObject.GetComponent<Text>() ;

		collected = new List<GameObject>();
		tempSavedSceneData = new Dictionary<string, SceneData>();
	}

	public void addToCollection(GameObject go)
    {
		collected.Add(go);
	}
	
	public void saveTemp()
    {
		SceneData sd = new SceneData();
		bool hasSaved = GameManager.GM.tempSavedSceneData.TryGetValue(SceneManager.GetActiveScene().name, out sd);
		if (!hasSaved)
		{
			List<float> collected_x = new List<float>();
			List<float> collected_y = new List<float>();
			foreach (GameObject go in collected)
			{
				collected_x.Add(go.transform.position.x);
				collected_y.Add(go.transform.position.y);
			}
			sd = new SceneData();
			sd.sceneName = SceneManager.GetActiveScene().name;
			sd.collectedSpiritPos_x = collected_x.ToArray();
			sd.collectedSpiritPos_y = collected_y.ToArray();

			tempSavedSceneData.Add(sd.sceneName, sd);
			collected = new List<GameObject>();
		}
		else
		{
			Debug.Log("sd.collectedSpiritPos_x.Length "+ sd.collectedSpiritPos_x.Length);
			List<float> collected_x = new List<float>();
			List<float> collected_y = new List<float>();
			foreach (GameObject go in collected)
			{
				collected_x.Add(go.transform.position.x);
				collected_y.Add(go.transform.position.y);
			}

			for (int i = 0; i < sd.collectedSpiritPos_x.Length; i++)
			{
				collected_x.Add(sd.collectedSpiritPos_x[i]);
				collected_y.Add(sd.collectedSpiritPos_y[i]);
			}

			sd = new SceneData();
			sd.sceneName = SceneManager.GetActiveScene().name;
			sd.collectedSpiritPos_x = collected_x.ToArray();
			sd.collectedSpiritPos_y = collected_y.ToArray();

			tempSavedSceneData[sd.sceneName] = sd;
			collected = new List<GameObject>();
		}
		
	}

	public void setGameData(GameData gameData)
    {
		this.gameData = gameData;
    }
	public GameData getGameData()
	{
		return gameData;
	}

	public void loadGameData()
	{
		gameTimer.TimerStop();
		tempSavedSceneData = new Dictionary<string, SceneData>();
		List<SceneData> list = gameData.arrSceneData;
		Debug.Log("list " + list.Count);
		foreach (SceneData sd in list)
		{
			Debug.Log("sceneName "+sd.sceneName);
			tempSavedSceneData.Add(sd.sceneName, sd);
		}
		loadAtCheckpoint = true;
		loadScene(gameData.sceneHasPlayer);
		PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
		playerHealth.HPmax = gameData.HPmax;
		playerHealth.resetState();
		score = gameData.score;
		player.transform.position = new Vector2(gameData.playerPos[0], gameData.playerPos[1]);
		player.GetComponent<PoolingItem>().resetParent();
		gameTimer.TimerStart(gameData.stopTime);


		scoreSpirit.text = ""+score;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isGameover && player.activeSelf)
		{
			if (GameIsPaused)
				resume();
			else
				pause();
		}
	}

    public void startGame()
    {
		collected = new List<GameObject>();
		if (firstTime) 
		{
			firstTime = false;
			GameObject playerPrefabs = Resources.Load<GameObject>("Prefabs/Player/Player");
			player = Instantiate(playerPrefabs);
			player.SetActive(false);			
			GameObject parentObject = new GameObject("GameObjects");
			player.AddComponent<PoolingItem>().setOriginalParent(parentObject.transform);
			DontDestroyOnLoad(parentObject);

			poolingManager.instantiateAllPool(parentObject);
		}

		isGameover = false;
		if(!testing)
			loadGameData();

		scoreSpirit.text = ""+ score;
	}
	public void loadScene(SceneName sn)
	{
		poolingManager.inactiveAll();
		sceneLoader.loadScene(sn);
		
	}
	public void loadScene(string strScene)
	{
		poolingManager.inactiveAll();
		sceneLoader.loadScene(strScene);

	}

	public void addScore(int amount)
	{

		score = score + amount;
		scoreSpirit.text = "" + score;
	}

	public void restart()
	{
		loadAtCheckpoint = true;
		setGameData(SaveSystem.loadData(gameData.filenumber));
		startGame();		
		GameOverMenu.SetActive(false);
		audioManager.SetActive(true);

	}
	public void gameover()
	{
		isGameover = true;
		gameTimer.TimerStop();
		GameOverMenu.SetActive(true);
		audioManager.SetActive(false);
	}
	public void resume()
	{
		gameTimer.TimerStart(gameTimer.getCurrentStopTime());		
		GameIsPaused = false;
		PauseMenu.SetActive(false);
		audioManager.SetActive(true);
	}

	public void pause()
	{
		gameTimer.TimerStop();
		GameIsPaused = true;
		PauseMenu.SetActive(true);
		audioManager.SetActive(false);
	}

}
