
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

    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    
    public void loadScene(SceneName sn)
    {
        SceneManager.LoadScene(sn.ToString());
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

        if (previousSceneName == "" && !equal(currentScene, SceneName.MainMenu))
        {
            GameManager.GM.startGame();
            GameManager.GM.player.SetActive(true);
        }
        # endregion

        if (equal(previousSceneName, SceneName.MainMenu))//  bat dau choi
        {
            GameManager.GM.startGame();
            GameManager.GM.player.SetActive(true);
            if (equal(currentScene, SceneName.SampleScene))
                GameManager.GM.player.transform.position = new Vector2(0, 0);
            else if(equal(currentScene, SceneName.Scene_8)) 
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

        Debug.Log("New scene loaded: "+ previousSceneName + " -> "+ currentScene.name);
        previousSceneName = currentScene.name;
        
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

    private bool equal(Scene s, SceneName sn)
    {
        return s.name == sn.ToString();
    }
    private bool equal(string s, SceneName sn)
    {
        return s == sn.ToString();
    }

}
