using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
 
    GameObject txtHelpersPanel;
    GameObject closeTutorialBtn;

    private void Awake()
    {
        //TODO rimuovere il find
        GameObject canvas = GameObject.Find("Canvas").gameObject;

        txtHelpersPanel = canvas.transform.Find("txtHelpersPanel").gameObject;
        closeTutorialBtn = canvas.transform.Find("closeTutorialBtn").gameObject;
    }

    public void Play()
    {
        SceneManager.LoadScene((int)Utility.Scene.LoadingScreen); 
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OpenTutorialPanel()
    {
        GameInstance.isTutorialMode = true;
        SceneManager.LoadScene((int)Utility.Scene.LoadingScreen);
    }

    public void PlaySfx()
    {
        GameInstance.ToggleSfx();
    }
    public void PlayMusic()
    {
        GameInstance.ToggleMusic();
        GameInstance.ToggleSfx();
    }
    public void ToggleQuality()
    {
        GameInstance.ToggleQuality();
    }

}
