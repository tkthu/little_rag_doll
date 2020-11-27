using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SceneData
{
    public string sceneName;
    public float[] collectedSpiritPos_x;
    public float[] collectedSpiritPos_y;
}

[System.Serializable]
public class GameData
{
    public int filenumber;
    public int score;
    public int HPmax;
    public string sceneHasPlayer;
    public float[] playerPos;
    public float stopTime;
    public List<SceneData> arrSceneData;

    public GameData()//Khởi tạo save file (lúc nhấn vào nút tạo mới save file)
    {
        score = 0;
        HPmax = 5;//????
        sceneHasPlayer = SceneName.Scene_1.ToString();

        playerPos = new float[2];
        playerPos[0] = -30f;
        playerPos[1] = -3f;

        stopTime = 0;

        arrSceneData = new List<SceneData>();
    }
    public GameData(GameManager GM)//tạo save file từ GameManager
    {
        filenumber = GM.getGameData().filenumber;
        score = GM.score;
        HPmax = GM.player.GetComponent<PlayerHealth>().HPmax;
        sceneHasPlayer = SceneManager.GetActiveScene().name;

        playerPos = new float[2];
        playerPos[0] = GM.player.transform.position.x;
        playerPos[1] = GM.player.transform.position.y;

        stopTime = GM.gameTimer.getCurrentTime();

        arrSceneData = new List<SceneData>();

        Dictionary<string, SceneData> dict = GM.tempSavedSceneData;
        Debug.Log("dict.Count " + dict.Count);
        foreach (KeyValuePair<string, SceneData> pair in dict)
        {
            arrSceneData.Add(pair.Value);
        }        
        
    }


}
