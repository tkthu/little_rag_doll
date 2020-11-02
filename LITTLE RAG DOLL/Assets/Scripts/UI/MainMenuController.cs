using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void btn_PlayHandler()
    {
        GameManager.GM.loadScene(SceneName.SaveFileScene);
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
