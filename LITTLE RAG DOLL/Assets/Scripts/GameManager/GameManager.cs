using System;
using System.Collections.Generic;
using UnityEngine;
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

	public Text scoreSpirit;

	[HideInInspector] public bool isGameover = false;
	[HideInInspector] public bool loadAtCheckpoint = false;
	[HideInInspector] public bool GameIsPaused = false;
	[HideInInspector] public GameObject GameUI;
	[HideInInspector] public GameObject PauseMenu;
	[HideInInspector] public GameObject GameOverMenu;

	private bool firstTime = true;


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
		interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("interactKey", "I"));
		eatShoot = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("eatShootKey", "L"));
		attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("attackKey", "J"));
		up = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("upKey", "W"));
		down = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("downKey", "S"));
		left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
		right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

		sceneLoader = GetComponent<SceneLoader>();
		poolingManager = GetComponent<PoolingManager>();
		gameTimer = GetComponent<GameTimer>();

		GameUI = transform.Find("GameUI").gameObject;
		PauseMenu = transform.Find("PauseMenu").gameObject;
		GameOverMenu = transform.Find("GameOverMenu").gameObject;

	}
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !isGameover)
		{
			if (GameIsPaused)
			{
				resume();
			}
			else
			{
				pause();
			}
		}


		#region for testing
		/*
		 * 1,2,3 save
		 * 4,5,6 load
		 * 7,8,9 delete
		 * 
		 */

		if (Input.GetKey(KeyCode.Keypad4) || Input.GetKey(KeyCode.Keypad5) || Input.GetKey(KeyCode.Keypad6))
		{
			int filenumber = 1;
			if (Input.GetKey(KeyCode.Keypad4))
				filenumber = 1;
			else if (Input.GetKey(KeyCode.Keypad5))
				filenumber = 2;
			else if (Input.GetKey(KeyCode.Keypad6))
				filenumber = 3;
			GameData gameData = SaveSystem.loadData(filenumber);

			if(gameData != null)
            {
				gameTimer.TimerStop();
				loadAtCheckpoint = true;
				loadScene(gameData.sceneHasPlayer);
				PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
				playerHealth.HPmax = gameData.HPmax;
				score = gameData.score;
				player.transform.position = new Vector2(gameData.playerPos[0], gameData.playerPos[1]);
				gameTimer.TimerStart(gameData.stopTime);

				scoreSpirit.text = "Spirit: " + score;
			}
			
		}
		if (Input.GetKey(KeyCode.Keypad7) || Input.GetKey(KeyCode.Keypad8) || Input.GetKey(KeyCode.Keypad9))
		{
			int filenumber = 1;
			if (Input.GetKey(KeyCode.Keypad7))
				filenumber = 1;
			else if (Input.GetKey(KeyCode.Keypad8))
				filenumber = 2;
			else if (Input.GetKey(KeyCode.Keypad9))
				filenumber = 3;
			SaveSystem.deleteData(filenumber);

		}

		
		#endregion
	}

    public void startGame()
    {
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

		score = 0;
		player.GetComponent<PlayerHealth>().resetState();

		gameTimer.TimerStart();

		scoreSpirit.text = "Spirit: "+ score;
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
		scoreSpirit.text = "Spirit: " + score;
	}

	public void restart()
	{
		loadAtCheckpoint = true;
		loadScene(SceneName.Scene_8);
		startGame();		
		GameOverMenu.SetActive(false);

	}
	public void gameover()
	{
		isGameover = true;
		gameTimer.TimerStop();
		GameOverMenu.SetActive(true);
	}
	public void resume()
	{
		gameTimer.TimerStart(gameTimer.getCurrentStopTime());		
		GameIsPaused = false;
		PauseMenu.SetActive(false);
	}

	public void pause()
	{
		gameTimer.TimerStop();
		GameIsPaused = true;
		PauseMenu.SetActive(true);
	}

}
