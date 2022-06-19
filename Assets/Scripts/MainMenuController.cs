using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    GameObject tutorialPanel;
    //Button _quitButton;
    private void Awake()
    {
        Destroy(GameObject.Find("Audio"));
        Destroy(GameObject.Find("MainCanvas"));

        //rimuvere il find
        GameObject canvas = GameObject.Find("Canvas").gameObject;
        tutorialPanel = canvas.transform.Find("TutorialPanel").gameObject;

        //_quitButton = GameObject.Find("exit").GetComponent<Button>();
    }

    private void Update()
    {
        HideIfClickedOutside(tutorialPanel);
    }

    public void Play()
    {
        SceneManager.LoadScene((int)Utility.Scene.LoadingScreen); 
    }
    public void Quit()
    {
        print("quit application");
        Application.Quit();
    }
    public void OpenTutorialPanel()
    {
        tutorialPanel.SetActive(true);
        //_quitButton.interactable = false;
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

    private void HideIfClickedOutside(GameObject panel)
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0) &&
            !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(), mousePosition))
        {
            panel.SetActive(false);
            StartCoroutine(ActivateButtonAfterTime());
        }
    }

    IEnumerator ActivateButtonAfterTime()
    {
        yield return new WaitForSeconds(1);
        //_quitButton.interactable = true;
    }
}
