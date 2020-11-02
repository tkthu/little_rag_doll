using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionsSceneController : MonoBehaviour
{
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public int[] screenWidths;
    int activeScreenResIndex = 0;

    void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("Screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("Fullscreen") == 1) ? true : false;

        volumeSliders[0].value = AudioManager.instance.masterVolumePercent;
        volumeSliders[1].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[2].value = AudioManager.instance.sfxVolumePercent;

        for (int i=0; i<resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }
        SetFullScreen(isFullscreen);
    }

    public void btn_BackMainMenu()
    {
        GameManager.GM.loadScene(SceneName.MainMenu);
    }
    public void btn_Controls ()
    {
        GameManager.GM.loadScene(SceneName.ControlsScene);
    }

    public void SetScreenResolution(int i)
    {
        if(resolutionToggles[i].isOn)
        {
            activeScreenResIndex = i;
            float aspecRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i]/aspecRatio), false);
            PlayerPrefs.SetInt("Screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
        
    }

    public void SetFullScreen(bool isFullscreen)
    {
        for(int i=0; i<resolutionToggles.Length;i++)
        {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if(isFullscreen)
        {
            Resolution[] allResolution = Screen.resolutions;
            Resolution maxResolution = allResolution[allResolution.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(activeScreenResIndex);
        }
        PlayerPrefs.SetInt("Fullscreen", ((isFullscreen)?1:0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}
