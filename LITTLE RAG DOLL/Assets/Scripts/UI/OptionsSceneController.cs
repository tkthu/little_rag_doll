using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void btn_ControlsHandler()
    {
        Debug.Log("vua nhan nut Controls");
        SceneManager.LoadScene("ControlsScene");
    }

    public void btn_BackHandler()
    {
        Debug.Log("vua nhan nut Back cua Scene Options");
        SceneManager.LoadScene("MainMenu");
    }
}
