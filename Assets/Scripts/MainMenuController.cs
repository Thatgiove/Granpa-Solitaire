using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameObject videoPlayer;
    GameObject txtHelpersPanel;
    GameObject closeTutorialBtn;

    private void Awake()
    {
        //TOFO rimuovere il find
        GameObject canvas = GameObject.Find("Canvas").gameObject;

        videoPlayer = canvas.transform.Find("videoPlayer").gameObject;
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
        videoPlayer?.SetActive(true);
        txtHelpersPanel?.SetActive(true);
        closeTutorialBtn?.SetActive(true);
    }

    public void PlaySfx()
    {
        GameInstance.ToggleSfx();
    }
    public void PlayMusic()
    {
        GameInstance.ToggleMusic();
    }
    public void ToggleQuality()
    {
        GameInstance.ToggleQuality();
    }

    public void closeTutorialPanel()
    {
        videoPlayer?.SetActive(false);
        txtHelpersPanel?.SetActive(false);
        closeTutorialBtn?.SetActive(false);
    }
}
