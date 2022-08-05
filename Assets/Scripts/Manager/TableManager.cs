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
    [SerializeField] TutorialManager tutorialManager;
    //////////PROPRIETA' DELL'IESIMO ELEMENTO DELLA TABLE POSITION//////////////////////
    public int cardCounter = 0;
    public int currentCardId = 0;
    public bool iSPrincipalCardline = false;
    public bool triggeringRow = false;
    public bool triggeringCol = false;

    public List<string> listOfCarfInTable = new List<string>();
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

        if (!_clickSound)
        {
            Debug.LogError("AudioClip not found");
        }
    }

    void Update()
    {
        if (card == null) return;

        if ((card.isPrincipalCard && triggeringRow) || (Input.GetMouseButtonUp(0) && triggeringRow))
        {
            if (card.isPrincipalCard)
            {
                iSPrincipalCardline = true;
                GameManager.PrincipalCardSeedList.Add(card.cardInfo.Description);
            }
            card.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.2f);
            card.canDrag = false;
            card.canPutOnTable = true;

            if (card.canPutOnTable && card.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(card); //rimuove ultima card dalla matrice
            }
            _deckManager.RemoveCardFromDecks(card);

            this.UpdateInfoOfTablePosition();
            if (GameInstance.isSfxPlaying)
            {
                if (GameManager.audioSource)
                    GameManager.audioSource.PlayOneShot(_clickSound);
                else
                {
                    Debug.LogError("AudioSource not found");
                }
            }

            if (GameInstance.isTutorialMode && !GameInstance.isSecondRuleSeen && !card.isPrincipalCard)
            {
                GameInstance.isSecondRuleSeen = true;
                tutorialManager?.OpenTutorialPanel(1);
            }

            triggeringRow = false;
        }
        else if (Input.GetMouseButtonUp(0) && triggeringCol)
        {
            if (iSPrincipalCardline)
                GameManager.PrincipalCardSeedList.Add(card.cardInfo.Description);

            //posso aggiungere alla lista di carte di quel gruppo


            //posizione della carta con offset
            card.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y - (cardCounter * 0.22f),
                gameObject.transform.position.z - (cardCounter * 0.3f));

            card.canPutOnTable = true;
            card.canDrag = false;



            if (card.canPutOnTable && card.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(card); //rimuove ultima card dalla matrice
            }

            _deckManager.RemoveCardFromDecks(card);
            this.UpdateInfoOfTablePosition();

            if (GameInstance.isSfxPlaying)
            {
                if(GameManager.audioSource)
                GameManager.audioSource.PlayOneShot(_clickSound);
                else
                {
                    Debug.LogError("AudioSource not found");
                }
            }

            if (GameInstance.isTutorialMode && !GameInstance.isThirdRuleSeen)
            {
                GameInstance.isThirdRuleSeen = true;
                tutorialManager?.SetThirdRule(card.cardInfo.Description);
                tutorialManager?.OpenTutorialPanel(2);
            }
     
            triggeringCol = false;
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

        //if (card.isPrincipalCard)
        //{
        //    iSPrincipalCardline = true;
        //    Manager.PrincipalCardSeedList.Add(card.cardInfo.Description);
        //    card.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.2f);
        //    card.canDrag = false;
        //    card.canPutOnTable = true;
        //    _soundEffect.Play();
        //}
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
        this.listOfCarfInTable.Add(card.cardInfo.Description);
    }

    bool CanPutInRow()
    {
        return card.cardInfo.Description == GameManager.PrincipalCardSeed && cardCounter < 1;
    }

    bool CanPutInCol()
    {
        return card.cardInfo.Id == currentCardId && cardCounter >= 1 &&
            (iSPrincipalCardline ||
            GameManager.PrincipalCardSeedList.Count > cardCounter && //previene l'outOfBound
            GameManager.PrincipalCardSeedList[cardCounter] == card.cardInfo.Description);  //la card description è la stessa di quella dalla principal card nella posizione del counter
    }
}
