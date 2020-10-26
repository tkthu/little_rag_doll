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
	private GameTimer gameTimer;
	[HideInInspector] public PoolingManager poolingManager;

	public Text scoreSpirit;

	[HideInInspector] public bool isGameover = false;
	[HideInInspector] public bool isRestartingScene = false;
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

		gameTimer.TimerReset();

		scoreSpirit.text = "Spirit: 0";
	}
	public void loadScene(SceneName sn)
	{
		poolingManager.inactiveAll();
		sceneLoader.loadScene(sn);
		
	}

    public void addScore(int amount)
	{
		score = score + amount;
		scoreSpirit.text = "Spirit: " + score;
	}

	public void restart()
	{
		isRestartingScene = true;
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
		Time.timeScale = 1f;
		GameIsPaused = false;
		PauseMenu.SetActive(false);
	}

	public void pause()
	{
		Time.timeScale = 0f;
		GameIsPaused = true;
		PauseMenu.SetActive(true);
	}

}
