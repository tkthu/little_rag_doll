using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioClip startGame;
    GameObject panel;

    private void Start()
    {
        panel = transform.Find("CreditsPanel").gameObject;
    }
    // Start is called before the first frame update
    public void btn_PlayHandler()
    {
        GameManager.GM.loadScene(SceneName.SaveFileScene);
    }
    public void btn_OptionsHandler()
    {
        GameManager.GM.loadScene(SceneName.OptionsScene);
    }

    public void btn_CreditsHandler()
    {
        panel.SetActive(true);
    }

    public void btn_HideCredits()
    {
        panel.SetActive(false);
    }

    public void btn_QuitHandler()
    {
        Application.Quit();
    }
    


}
