using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void btn_PlayHandler()
    {
        GameManager.GM.loadScene(SceneName.Scene_8);
    }
    public void btn_OptionsHandler()
    {
        GameManager.GM.loadScene(SceneName.OptionsScene);
    }
    public void btn_QuitHandler()
    {
        Application.Quit();
    }
    


}
