using System.Collections.Generic;
using UnityEditor.iOS;
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

	public GameObject player;

	private List<GameObject> pooledStraightBullets;
	public int StraightBulletsNo;

	private List<GameObject> pooledPlayerStraightBullets;
	public int PlayerStraightBulletsNo;

	private List<GameObject> pooledBounceBullets;
	public int BounceBulletssNo;

	private List<GameObject> pooledPlayerBounceBullets;
	public int PlayerBounceBulletsNo;
	/*
	private List<GameObject> pooledExplodeBullets;
	public int ExplodeBulletsNo;
	*/
	private List<GameObject> pooledPlayerExplodeBullets;
	public int PlayerExplodeBulletsNo;


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


		player = Instantiate(player);
		player.SetActive(false);
		DontDestroyOnLoad(player);

		string bulletPrefabPath = "Assets/Prefabs/Bullets/";
		GameObject StraightBullet = Resources.Load<GameObject>( bulletPrefabPath + "StraightBullet.prefab");
		instantiatePool(StraightBullet,StraightBulletsNo,pooledStraightBullets);

		Debug.Log(pooledStraightBullets.Count);

	}

	private void instantiatePool(GameObject objectToPool, int amountToPool,List<GameObject> pooledObjects)
    {
		GameObject tmp;
		for (int i = 0; i < amountToPool; i++)
        {
			tmp = Instantiate(objectToPool);
			tmp.SetActive(false);
			pooledObjects.Add(tmp);
        }
    }


	private void OnLevelWasLoaded(int level)// chỉ dùng cho debug. sau này sẽ xóa !!!
    {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject go in gos)
        {
			if (go != player)
			Destroy(go);
		}
			

	}
}
