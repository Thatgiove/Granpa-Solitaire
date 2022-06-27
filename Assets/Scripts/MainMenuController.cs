using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenuController : MonoBehaviour
{
    GameObject videoPlayer;
    GameObject txtHelpersPanel;
    GameObject closeTutorialBtn;

    private void Awake()
    {
        //TODO rimuovere il find
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
        PlayVideo();

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
        ResetVideo();

        txtHelpersPanel?.SetActive(false);
        closeTutorialBtn?.SetActive(false);
    }

    void ResetVideo()
    {
        var vp = videoPlayer?.GetComponent<VideoPlayer>();

        RenderTexture.active = vp.targetTexture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = null;
        vp.Stop();
    }

    void PlayVideo()
    {
        var vp = videoPlayer?.GetComponent<VideoPlayer>();
        if (vp)
        {
            vp.Play();
        }   
    }
}
