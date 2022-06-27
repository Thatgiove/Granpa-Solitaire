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

        SceneManager.LoadScene((int)Utility.Scene.GameTable);
    }
    public void MainMenu()
    {
        GameManager.PrincipalCardSeedList = new List<string>();
        SceneManager.LoadScene((int)Utility.Scene.MainMenu);
    }

    public void ToggleSound()
    {
        GameInstance.ToggleMusic();
    }

    public void ControlDeck()
    {
        GameObject.Find("DeckManager").GetComponent<DeckManager>().SwipeCardDeck();
    }
}

