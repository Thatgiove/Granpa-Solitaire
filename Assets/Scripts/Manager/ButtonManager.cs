using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestarGame()
    {
        Manager.SeedList = new List<string>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Manager.SeedList = new List<string>();
        SceneManager.LoadScene("Main Menu");
    }
}

