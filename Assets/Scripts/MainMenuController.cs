using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameObject tutorialPanel;
    GameObject QuitButton;
    private void Awake()
    {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
