using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        GameInstance.isTutorialMode = false;

        GameInstance.isFirstRuleSeen = false;
        GameInstance.isSecondRuleSeen = false;
        GameInstance.isThirdRuleSeen = false;
    }

    public void ToggleSound()
    {
        GameInstance.ToggleMusic();
        GameInstance.ToggleSfx();
    }

    public void ControlDeck()
    {
        GameObject.Find("DeckManager").GetComponent<DeckManager>().SwipeCardDeck();
    }

    void PreviousMove()
    {
        var previousMove = GameInstance.previousMove;

        if (previousMove.card.isPrincipalCard || 
            (!previousMove.card.isMatrix && previousMove.deckIndex == -1)) return;

        previousMove.card.transform.localPosition = previousMove.oldPosition;
        previousMove.card.canDrag = true;
        previousMove.card.canPutOnTable = false;

        //resetto il tableManager
        previousMove.tableManager.listOfCardInTable.Remove(previousMove.card.cardInfo.Description);
        previousMove.tableManager.cardCounter--;
        if (previousMove.tableManager.listOfCardInTable.Count <= 0)
        {
            previousMove.tableManager.currentCardId = 0;
        }
        if (previousMove.tableManager.iSPrincipalCardline)
        {
            GameManager.PrincipalCardSeedList.Remove(previousMove.card.cardInfo.Description);
        }
        ////
        //la rimetto nella matrice
        if (previousMove.card.isMatrix) 
        {
            FindObjectOfType<MatrixManager>()?.PutInMatrix(previousMove.card);
        }
        //la rimetto nel mazzo
        else
        {
            FindObjectOfType<DeckManager>()?.Deck_tmp.Insert(previousMove.deckIndex, previousMove.card);
        }

        //disabilita il bottone
        GetComponent<Button>().interactable = false;
        var img = GetComponent<Button>().transform.GetChild(0).GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, .3f); 
    }
}

