using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
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
        Debug.Log("vua nhan nut Play");
        SceneManager.LoadScene("SampleScene");
        
    }
    public void btn_OptionsHandler()
    {
        Debug.Log("vua nhan nut Options");
        SceneManager.LoadScene("OptionsScene");
    }
    public void btn_QuitHandler()
    {
        Debug.Log("vua nhan nut Quit to Desktop");
        Application.Quit();
    }
}
