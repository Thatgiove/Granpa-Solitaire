using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void RestarGame()
    {
        GameManager.PrincipalCardSeedList = new List<string>();
        GameManager.DeckEmpty = false;
        GameManager.MatrixEmpty = false;
        //TODO creare un AudioManager statico
        GameObject.Find("Audio").GetComponent<AudioSingleton>().PlayMusic();
        SceneManager.LoadScene((int)Utility.Scene.GameTable);
    }
    public void MainMenu()
    {
        GameManager.PrincipalCardSeedList = new List<string>();
        SceneManager.LoadScene((int)Utility.Scene.MainMenu);
    }

    public void ToggleSound()
    {
        GameManager.ShouldPlaySound = !GameManager.ShouldPlaySound;
        GameObject.Find("Audio").GetComponent<AudioSingleton>().PlayMusic();
    }

    public void ControlDeck()
    {
        GameObject.Find("DeckManager").GetComponent<DeckManager>().SwipeCardDeck();
    }
}

