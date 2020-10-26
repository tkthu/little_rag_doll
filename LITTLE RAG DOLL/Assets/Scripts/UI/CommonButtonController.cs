using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonButtonController : MonoBehaviour
{
    public void restart()
    {
        GameManager.GM.restart();
    }

    public void resume()
    {
        GameManager.GM.resume();
    }

    public void backToMenu()
    {
        GameManager.GM.resume();
        GameManager.GM.loadScene(SceneName.MainMenu);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
