using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_PlayHandler()
    {        
        SceneManager.LoadScene("SampleScene");
        GameManager.GM.player.SetActive(true);
    }
    public void btn_OptionsHandler()
    {
        SceneManager.LoadScene("OptionsScene");
    }
    public void btn_QuitHandler()
    {
        Application.Quit();
    }
    


}
