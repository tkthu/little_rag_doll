﻿
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
    Scene_1,
    Scene_2,
    Scene_2a,
    Scene_3,
    Scene_4,
    Scene_4a,
    Scene_4b,
    Scene_5,
    Scene_5a,
    Scene_5b,
    Scene_5c,
    Scene_5d,
    Scene_6,
    Scene_7,
    Scene_8,
    Scene_9,
    Scene_10,
    Scene_11,
    Win,
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
        loadCollectables();

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
        else if (equal(currentScene, SceneName.Win))
        {
            GameManager.GM.GameUI.SetActive(false);
            GameManager.GM.PauseMenu.SetActive(false);
            GameManager.GM.GameOverMenu.SetActive(false);
            if (GameManager.GM.player != null)
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

    private void loadCollectables()
    {
        GameObject holder = GameObject.FindGameObjectWithTag("CollectablesHolder");
        if (holder != null)
        {
            foreach (Transform child in holder.transform)
            {
                SceneData sd = new SceneData();
                bool hasSaved = GameManager.GM.tempSavedSceneData.TryGetValue(SceneManager.GetActiveScene().name, out sd);
                if (hasSaved)
                {
                    for (int i = 0; i < sd.collectedSpiritPos_x.Length; i++)
                    {
                        if (child.position.x == sd.collectedSpiritPos_x[i] && child.position.y == sd.collectedSpiritPos_y[i])
                        {
                            child.gameObject.SetActive(false);
                            break;
                        }
                    }

                }
            }
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

                }                
                go.SetActive(true);
                go.transform.GetComponent<EnemyHealth>().respawnPos = child.position;
                go.transform.position = child.position;
                Attachment attachmentComponent = child.GetComponent<Attachment>();
                if (attachmentComponent != null)
                {
                    go.AddComponent<Attachment>().attachment = attachmentComponent.attachment;
                }
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
