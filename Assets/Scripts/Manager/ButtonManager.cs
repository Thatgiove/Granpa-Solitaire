using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    //bool musicPlaying = true;


    public void RestarGame()
    {
        Manager.PrincipalCardSeedList = new List<string>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void MainMenu()
    {
        Manager.PrincipalCardSeedList = new List<string>();
        SceneManager.LoadScene("Main Menu");
    }

    public void ToggleSound()
    {
        Manager.musicPlaying = !Manager.musicPlaying;
        GameObject.Find("SceneController").GetComponent<SceneController>().PlayMusic();
    }
}

