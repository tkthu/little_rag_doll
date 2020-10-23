using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData gameData = new GameData();

        Debug.Log("saveData"
                + " - gameData.score " + gameData.score
                + " - gameData.HPmax " + gameData.HPmax
                + " - gameData.sceneHasPlayer " + gameData.sceneHasPlayer
                + " - gameData.playerPos " + gameData.playerPos[0] + " " + gameData.playerPos[1]
                + " - gameData.arrSceneData " + gameData.arrSceneData
                ) ;
        foreach (SceneData sd in gameData.arrSceneData)
        {
            Debug.Log(sd.sceneName
                + sd.collectedSpiritPos_x[0]
                + sd.collectedSpiritPos_x[1]
                + sd.collectedSpiritPos_y[0]
                + sd.collectedSpiritPos_y[1]
                );
        }

        formatter.Serialize(stream, gameData);
        stream.Close();
    }

    public static GameData loadData()
    {
        string path = Application.persistentDataPath + "/GameData.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData gameData = formatter.Deserialize(stream) as GameData;

            stream.Close();
            Debug.Log("saveData"
                + " - gameData.score " + gameData.score
                + " - gameData.HPmax " + gameData.HPmax
                + " - gameData.sceneHasPlayer " + gameData.sceneHasPlayer
                + " - gameData.playerPos " + gameData.playerPos[0] + " " + gameData.playerPos[1]
                + " - gameData.arrSceneData " + gameData.arrSceneData
                );
            foreach (SceneData sd in gameData.arrSceneData)
            {
                Debug.Log(sd.sceneName
                    + sd.collectedSpiritPos_x[0]
                    + sd.collectedSpiritPos_x[1]
                    + sd.collectedSpiritPos_y[0]
                    + sd.collectedSpiritPos_y[1]
                    );
            }
            return gameData;
        }
        else
        {
            Debug.LogError("SAve file nor found in " + path) ;
            return null;
        }
    }
}
