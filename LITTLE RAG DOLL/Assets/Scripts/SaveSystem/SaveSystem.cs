using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void saveData(int number)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/GameData_"+number+".dat";
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

    public static GameData loadData(int number)
    {
        string path = Application.persistentDataPath + "/GameData_" + number + ".dat";
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
                + " - gameData.stopTime " + gameData.stopTime
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
            Debug.LogWarning("loadData: Save file nor found in " + path) ;
            return null;
        }
    }
    public static void deleteData(int number)
    {
        string path = Application.persistentDataPath + "/GameData_" + number + ".dat";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        else
        {
            Debug.LogWarning("deleteData: Save file nor found in " + path);
        }
        
    }
}
