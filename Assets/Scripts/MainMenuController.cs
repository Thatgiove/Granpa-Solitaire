using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameObject tutorialPanel;
    GameObject QuitButton;
    private void Awake()
    {
        Destroy(GameObject.Find("Audio"));
        Destroy(GameObject.Find("MainCanvas"));


        //rimuvere il find
        GameObject canvas = GameObject.Find("Canvas").gameObject;
        tutorialPanel = canvas.transform.Find("TutorialPanel").gameObject;

        QuitButton = GameObject.Find("Exit");
    }

    private void Update()
    {
        HideIfClickedOutside(tutorialPanel, QuitButton);
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
        tutorialPanel.SetActive(true);
        QuitButton.SetActive(false);
    }


    private void HideIfClickedOutside(GameObject panel, GameObject quitButtton)
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0) &&
            !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(), mousePosition))
        {
            panel.SetActive(false);
            quitButtton.SetActive(true);
        }


    }
}
