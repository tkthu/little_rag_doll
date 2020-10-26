﻿
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
    MainMenu,
    ControlsScene,
    OptionsScene,
    SampleScene,
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

    private void onSceneLoaded(Scene currentScene, LoadSceneMode loadSceneMode)
    {
        UIShowing(currentScene);

        if (equal(previousSceneName, SceneName.MainMenu) && !equal(currentScene, SceneName.ControlsScene ) && !equal(currentScene, SceneName.OptionsScene))//  bat dau choi
        {
            GameManager.GM.startGame();
            GameManager.GM.player.SetActive(true);
            if(equal(currentScene, SceneName.Scene_8)) 
                GameManager.GM.player.transform.position = new Vector2(-4, -7.5f);
        }
        else
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

        #region for testing
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos)
        {
            if (go != GameManager.GM.player)
                Destroy(go);
        }

        if (previousSceneName == "" && !equal(currentScene, SceneName.MainMenu))
        {
            GameManager.GM.startGame();
            GameManager.GM.player.SetActive(true);
        }
        if (equal(currentScene, SceneName.Scene_8) && GameManager.GM.isRestartingScene)
        {
            GameManager.GM.player.transform.position = new Vector2(-4, -7.5f);
            GameManager.GM.isRestartingScene = false;
        }

        #endregion
    }

    private void UIShowing(Scene currentScene)
    {
        if (equal(currentScene, SceneName.MainMenu) || equal(currentScene, SceneName.ControlsScene) || equal(currentScene, SceneName.OptionsScene))
        {
            GameManager.GM.GameUI.SetActive(false);
            GameManager.GM.PauseMenu.SetActive(false);
            GameManager.GM.GameOverMenu.SetActive(false);
        }
        else
        {
            GameManager.GM.GameUI.SetActive(true);
            GameManager.GM.PauseMenu.SetActive(false);
            GameManager.GM.GameOverMenu.SetActive(false);
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
