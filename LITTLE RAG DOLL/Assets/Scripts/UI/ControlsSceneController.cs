using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlsSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void btn_BackHandler()
    {
        Debug.Log("vua nhan nut Back cua Scene Controls");
        SceneManager.LoadScene("OptionsScene");
    }
}
