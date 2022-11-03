using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
///NEL TAVOLO DA GIOCO SONO PRESENTI 10 POSIZIONI
///OGNI POSIZIONE HA DELLE PROPRIETA' CHE IDENTIFICANO
///QUANTE CARTE SONO PRESENTI, QUALI SEMI SONO USCITI E SE
///E' LA POSIZIONE DELLA CARTA PRINCIPALE. IN BASE A
///QUESTE PROPRIETA' VENGONO EFFETTUATI DEI CONTROLLI
///NEI METODI CanPutInRow() E CanPutInCol()
/// </summary>


public class TableManager : MonoBehaviour
{
    TutorialManager tutorialManager;
    //////////PROPRIETA' DELL'IESIMO ELEMENTO DELLA TABLE POSITION//////////////////////
    public int cardCounter = 0;
    public int currentCardId = 0;
    public bool iSPrincipalCardline = false;
    public bool triggeringRow = false;
    public bool triggeringCol = false;
    public bool isMatrix = false;

    public List<string> listOfCardInTable = new List<string>();
    /////////////////////////////////////////////////////////////////////////////////

    Card card; // la carta che collide
    DeckManager _deckManager;
    MatrixManager _matrixManager;
    AudioClip _clickSound;

    void Start()
    {
        _deckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        _matrixManager = GameObject.Find("Matrix").GetComponent<MatrixManager>();
        _clickSound = (AudioClip)Resources.Load("Audio/click");
        tutorialManager = Resources.FindObjectsOfTypeAll<TutorialManager>()?[0];

        if (!_clickSound)
        {
            Debug.LogError("AudioClip not found");
        }
    }

    void Update()
    {
        if (card == null) return;

        //Metto in riga
        if ((card.isPrincipalCard && triggeringRow) || (Input.GetMouseButtonUp(0) && triggeringRow))
        {
            if (card.isPrincipalCard)
            {
                iSPrincipalCardline = true;
                GameManager.PrincipalCardSeedList.Add(card.cardInfo.Description);
            }

            SetPreviousMove();

            card.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z - 0.2f);

            card.canDrag = false;
            card.canPutOnTable = true;

            //rimuove ultima card dalla matrice e dal mazzo
            if (card.canPutOnTable && card.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(card); 
            }
            _deckManager.RemoveCardFromDecks(card);

            this.UpdateInfoOfTablePosition();

            if (GameInstance.isSfxPlaying && GameManager.audioSource)
            {
                GameManager.audioSource.PlayOneShot(_clickSound);
            }

            if (GameInstance.isTutorialMode && !GameInstance.isSecondRuleSeen && !card.isPrincipalCard)
            {
                GameInstance.isSecondRuleSeen = true;
                tutorialManager?.OpenTutorialPanel(1);
            }

            triggeringRow = false;
            card.GetComponent<Animator>()?.SetTrigger("CardShine");
        }

        //Metto in colonna
        else if (Input.GetMouseButtonUp(0) && triggeringCol)
        {
            if (iSPrincipalCardline)
                GameManager.PrincipalCardSeedList.Add(card.cardInfo.Description);

            SetPreviousMove();

            //posso aggiungere alla lista di carte di quel gruppo
            //posizione della carta con offset
            card.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y - (cardCounter * 0.17f),
                gameObject.transform.position.z - (cardCounter * 0.3f));

            card.canPutOnTable = true;
            card.canDrag = false;

            if (card.canPutOnTable && card.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(card);
            }
            _deckManager.RemoveCardFromDecks(card);
            this.UpdateInfoOfTablePosition();

            if (GameInstance.isSfxPlaying && GameManager.audioSource)
            {
                GameManager.audioSource.PlayOneShot(_clickSound);
            }

            if (GameInstance.isTutorialMode && !GameInstance.isThirdRuleSeen)
            {
                GameInstance.isThirdRuleSeen = true;
                tutorialManager?.SetThirdRule(card.cardInfo.Description);
                tutorialManager?.OpenTutorialPanel(2);
            }
     
            triggeringCol = false;

            card.GetComponent<Animator>()?.SetTrigger("CardShine");
        }
         
    }

    private void OnTriggerExit(Collider other)
    {
        triggeringRow = false;
        triggeringCol = false;
    }

    void OnTriggerEnter(Collider other)
    {
        card = other.gameObject.GetComponent<Card>();

        if (CanPutInRow())
        {
            triggeringRow = true;
        }
        else if (CanPutInCol()) 
        {
            triggeringCol = true;
        }

    }

    void UpdateInfoOfTablePosition()
    {
        this.cardCounter += 1;
        this.currentCardId = card.cardInfo.Id;
        this.listOfCardInTable.Add(card.cardInfo.Description);
    }

    public bool CanPutInRow(Card _card = null)
    {
        if (!_card) _card = card;
        return _card.cardInfo.Description == GameManager.PrincipalCardSeed && cardCounter < 1;
    }

    public bool CanPutInCol(Card _card = null)
    {
        if (!_card) _card = card;
        return _card.cardInfo.Id == currentCardId && cardCounter >= 1 &&
            (iSPrincipalCardline ||
            GameManager.PrincipalCardSeedList.Count > cardCounter && //previene l'outOfBound
            GameManager.PrincipalCardSeedList[cardCounter] == _card.cardInfo.Description);  //la card description è la stessa di quella dalla principal card nella posizione del counter
    }

    void SetPreviousMove()
    {
        GameInstance.previousMove.card = card;
        GameInstance.previousMove.tableManager = GetComponent<TableManager>();
        GameInstance.previousMove.oldPosition = card.transform.localPosition;

        if(card.canPutOnTable && card.isMatrix)
        {
            GameInstance.previousMove.cardMatrix = _matrixManager.cardMatrix;
        }
    }
}
