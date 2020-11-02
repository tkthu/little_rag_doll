
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    MainMenu,
    SaveFileScene,
    ControlsScene,
    OptionsScene,
    SampleScene,
    Scene_4,
    Scene_5,
    Scene_6,
    Scene_6a,
    Scene_7,
    Scene_8,
    Scene_8a,
    Scene_8b,
    Scene_9,
    Scene_9a,
    Scene_9b,
    Scene_9c,
    Scene_9d,
    Scene_10,
    Scene_11,
    Scene_12,
    Scene_13,
    Scene_14,
    Scene_15,
    Scene_16,
}

public class SceneLoader : MonoBehaviour
{
    string previousSceneName = "";

    void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    public void loadScene(SceneName sn)
    {
        SceneManager.LoadScene(sn.ToString());
    }
    public void loadScene(string strScene)
    {
        SceneManager.LoadScene(strScene);
    }

    private void onSceneLoaded(Scene currentScene, LoadSceneMode loadSceneMode)
    {
        #region for testing
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos)
        {
            if (go != GameManager.GM.player)
                Destroy(go);
        }

        if (previousSceneName == "" && equal(currentScene, SceneName.SampleScene))
        {
            GameManager.GM.testing = true;
            GameManager.GM.startGame();
            GameManager.GM.player.SetActive(true);
            
        }
        #endregion

        UIShowing(currentScene);
        
        if (GameManager.GM.loadAtCheckpoint)
        {
            GameManager.GM.loadAtCheckpoint = false;
        }else
        {
            GameObject[] sceneTriggers = GameObject.FindGameObjectsWithTag("SceneTrigger");
            foreach (GameObject st in sceneTriggers)
            {
                if (st.GetComponent<SceneTrigger>().toScene.ToString() == previousSceneName)
                    GameManager.GM.player.transform.position = st.transform.position;
            }
        }
        loadEnemies();
        loadInteractables();

        Debug.Log("New scene loaded: "+ previousSceneName + " -> "+ currentScene.name);
        previousSceneName = currentScene.name;

    }

    private void UIShowing(Scene currentScene)
    {
        if (equal(currentScene, SceneName.MainMenu) || equal(currentScene, SceneName.ControlsScene) || equal(currentScene, SceneName.OptionsScene) || equal(currentScene, SceneName.SaveFileScene))
        {
            GameManager.GM.GameUI.SetActive(false);
            GameManager.GM.PauseMenu.SetActive(false);
            GameManager.GM.GameOverMenu.SetActive(false);
            if(GameManager.GM.player != null)
                GameManager.GM.player.SetActive(false);
        }
        else
        {
            GameManager.GM.GameUI.SetActive(true);
            GameManager.GM.PauseMenu.SetActive(false);
            GameManager.GM.GameOverMenu.SetActive(false);
            GameManager.GM.player.SetActive(true);
        }
    }

    private void loadEnemies()
    {
        GameObject holder = GameObject.FindGameObjectWithTag("EnemiesHolder");
        if (holder != null)
        {
            
            foreach (Transform child in holder.transform)
            {
                GameObject go = child.gameObject;
                switch (child.tag)
                {
                    case "Bat":
                        go = GameManager.GM.poolingManager.getBat();
                        break;
                    case "Snail":
                        go = GameManager.GM.poolingManager.getSnail();
                        break;
                    case "Fairy":
                        go = GameManager.GM.poolingManager.getFairy();
                        break;
                    case "BubbleBlower":
                        go = GameManager.GM.poolingManager.getBubbleBlower();
                        break;
                    case "Frog":
                        go = GameManager.GM.poolingManager.getFrog();
                        break;

                }                
                go.SetActive(true);
                go.transform.GetComponent<EnemyHealth>().respawnPos = child.position;
                go.transform.position = child.position;
                Destroy(child.gameObject);   
            }
            Destroy(holder);
        }

    }

    private void loadInteractables()
    {
        GameObject holder = GameObject.FindGameObjectWithTag("InteractablesHolder");
        if (holder != null)
        {
            foreach (Transform child in holder.transform)
            {
                GameObject go = child.gameObject;
                switch (child.tag)
                {
                    case "Flower":
                        go = GameManager.GM.poolingManager.getFlower();
                        break;
                    case "Helmet":
                        go = GameManager.GM.poolingManager.getHelmet();
                        break;

                }
                go.SetActive(true);
                if(go.transform.GetComponent<FlowerHealth>() != null)
                    go.transform.GetComponent<FlowerHealth>().respawnPos = child.position;
                go.transform.position = child.position;
                Destroy(child.gameObject);
            }
            Destroy(holder);
        }

    }

    private bool equal(Scene s, SceneName sn)
    {
        return s.name == sn.ToString();
    }
    private bool equal(string s, SceneName sn)
    {
        return s == sn.ToString();
    }

}
