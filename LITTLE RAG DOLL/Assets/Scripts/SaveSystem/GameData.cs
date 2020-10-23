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
    
    public int score;
    public int HPmax;
    public string sceneHasPlayer;
    public float[] playerPos;
    public List<SceneData> arrSceneData;


    public GameData()
    {
        score = GameManager.GM.score;
        HPmax = GameManager.GM.player.GetComponent<PlayerHealth>().HPmax;
        sceneHasPlayer = SceneManager.GetActiveScene().name;
        playerPos = new float[2];
        playerPos[0] = GameManager.GM.player.transform.position.x;
        playerPos[1] = GameManager.GM.player.transform.position.y;

        //dummy
        SceneData sd = new SceneData();
        sd.sceneName = SceneName.Scene_8.ToString();
        sd.collectedSpiritPos_x = new float[2];
        sd.collectedSpiritPos_y = new float[2];
        sd.collectedSpiritPos_x[0] = 0;
        sd.collectedSpiritPos_x[1] = 4;
        sd.collectedSpiritPos_y[0] = 1;
        sd.collectedSpiritPos_y[1] = 1;

        arrSceneData = new List<SceneData>();
        arrSceneData.Add(sd);

    }

    public GameData(int score, int hPmax, string sceneHasPlayer, float[] playerPos , List<SceneData> arrSceneData)
    {
        this.score = score;
        HPmax = hPmax;
        this.sceneHasPlayer = sceneHasPlayer;
        this.playerPos[0] = playerPos[0];
        this.playerPos[1] = playerPos[1];

        //dummy
        SceneData sd = new SceneData();
        arrSceneData = new List<SceneData>();
        foreach (SceneData asd in arrSceneData)
        {
            sd.sceneName = asd.sceneName;
            sd.collectedSpiritPos_x = new float[2];
            sd.collectedSpiritPos_y = new float[2];
            sd.collectedSpiritPos_x[0] = asd.collectedSpiritPos_x[0];
            sd.collectedSpiritPos_x[1] = asd.collectedSpiritPos_x[1];
            sd.collectedSpiritPos_y[0] = asd.collectedSpiritPos_y[0];
            sd.collectedSpiritPos_y[1] = asd.collectedSpiritPos_y[1];

            arrSceneData.Add(sd);
        }
        
        
    }



}
