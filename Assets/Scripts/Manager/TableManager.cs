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

    //////////PROPRIETA' DELL'IESIMO ELEMENTO DELLA TABLE POSITION//////////////////////
    int cardCounter = 0;
    int currentCardId = 0;
    bool iSPrincipalCardline = false;
    List<string> listOfCarfInTable = new List<string>();
    /////////////////////////////////////////////////////////////////////////////////

    Card card; // la carta che collide
    DeckManager _deckManager;
    MatrixManager _matrixManager;
    AudioSource _soundEffect;

    void Start()
    {
        _deckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        _matrixManager = GameObject.Find("Matrix").GetComponent<MatrixManager>();

        _soundEffect = GameObject.Find("ClickEffect").GetComponent<AudioSource>();

    }


    void OnTriggerEnter(Collider other)
    {
        card = other.gameObject.GetComponent<Card>();


        if (CanPutInRow())
        {
            card.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.2f);
            card.canDrag = false;
            card.canPutOnTable = true;

            if (card.canPutOnTable && card.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(card); //rimuove ultima card dalla matrice
            }
            _deckManager.RemoveCardFromDecks(card);


            if (card.isPrincipalCard)
            {
                //aggiungo alla lista di stringhe il seme
                iSPrincipalCardline = true;
                Manager.PrincipalCardSeedList.Add(card.cardInfo.Description);
            }


            this.UpdateInfoOfTablePosition();


            _soundEffect.Play();

        }

        
        else if (CanPutInCol()) 
        {
           
            if (iSPrincipalCardline)
                Manager.PrincipalCardSeedList.Add(card.cardInfo.Description);

            //posso aggiungere alla lista di carte di quel gruppo


            //posizione della carta con offset
            card.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y - (cardCounter * 0.2f),
                gameObject.transform.position.z - (cardCounter * 0.3f));

            card.canPutOnTable = true;
            card.canDrag = false;

            

            if (card.canPutOnTable && card.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(card); //rimuove ultima card dalla matrice
            }

            _deckManager.RemoveCardFromDecks(card);
            this.UpdateInfoOfTablePosition();

            _soundEffect.Play();

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
        return card.cardInfo.Description == Manager.PrincipalCardSeed && cardCounter < 1;
    }

    bool CanPutInCol()
    {
        return card.cardInfo.Id == currentCardId && cardCounter >= 1 &&
            (iSPrincipalCardline ||
            Manager.PrincipalCardSeedList.Count > cardCounter && //previene l'outOfBound
            Manager.PrincipalCardSeedList[cardCounter] == card.cardInfo.Description);  //la card description è la stessa di quella dalla principal card nella posizione del counter
    }


}
