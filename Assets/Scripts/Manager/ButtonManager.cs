using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{


    public void RestarGame()
    {

        Manager.PrincipalCardSeedList = new List<string>();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void MainMenu()
    {
        Manager.PrincipalCardSeedList = new List<string>();
        Destroy(GameObject.Find("Audio"));
        Destroy(GameObject.Find("Canvas"));
        SceneManager.LoadScene("Main Menu");
    }

    public void ToggleSound()
    {
        Manager.PlayMusic = !Manager.PlayMusic;
        GameObject.Find("Audio").GetComponent<AudioSingleton>().PlayMusic();
    }

    public void ControlDeck()
    {
        GameObject.Find("DeckManager").GetComponent<DeckManager>().SwipeCardDeck();

    }

}

