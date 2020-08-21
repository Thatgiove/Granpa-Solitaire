using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    //determina se nel collider è già presente una carta
    //TODO deve essere privato
    public int cardCounter = 0;
    public int currentCardId = 0;
    public List<string> listOfCarfInTable;
    public bool iSPrincipalCardline = false;
    DeckManager _deckManager;
    MatrixManager _matrixManager;
    AudioSource _soundEffect;

    void Start()
    {
        //Debug.Log("tableposition ");
        _deckManager = GameObject.Find("Button").GetComponent<DeckManager>();
        _matrixManager = GameObject.Find("Matrix").GetComponent<MatrixManager>();
        _soundEffect = GameObject.Find("BUTTON_Click_Jigsaw_Crop_01_stereo").GetComponent<AudioSource>();

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Card cardTemplate = other.gameObject.GetComponent<Card>();


        //POSSO METTERE IN RIGA
        if (cardTemplate.cardInfo.Description == Manager.CardSeed && cardCounter < 1)
        {
            cardTemplate.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z - 0.2f);

            cardTemplate.canDrag = false;
            cardTemplate.canPutOnTable = true;
            RemoveFromDeck(cardTemplate);

            if (cardTemplate.canPutOnTable && cardTemplate.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(cardTemplate); //rimuove ultima card dalla matrice
            }


            cardCounter += 1;
            currentCardId = cardTemplate.cardInfo.Id;

            if (cardTemplate.isPrincipalCard)
            {
                //aggiungo alla lista di stringhe il seme
                iSPrincipalCardline = true;
                Manager.SeedList.Add(cardTemplate.cardInfo.Description);
            }

            listOfCarfInTable.Add(cardTemplate.cardInfo.Description);
            _soundEffect.Play();

        }

        //POSSO METTERE IN COLONNA
        //se la carta che collide 
        else if (cardTemplate.cardInfo.Id == currentCardId && cardCounter >= 1 &&
            (iSPrincipalCardline ||
            Manager.SeedList.Count > cardCounter && //previene l'outOfBound
            Manager.SeedList[cardCounter] == cardTemplate.cardInfo.Description))  //la card description è la stessa di quella dalla principal card nella possizione del counter
        {
            if (iSPrincipalCardline)
                Manager.SeedList.Add(cardTemplate.cardInfo.Description);

            //posso aggiungere alla lista di carte di quel gruppo


            //posizione della carta con offset
            cardTemplate.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y - (cardCounter * 0.2f),
                gameObject.transform.position.z - (cardCounter * 0.3f));

            cardTemplate.canPutOnTable = true;
            cardTemplate.canDrag = false;

            RemoveFromDeck(cardTemplate);

            if (cardTemplate.canPutOnTable && cardTemplate.isMatrix)
            {
                _matrixManager.RemoveFromMatrix(cardTemplate); //rimuove ultima card dalla matrice
            }
            cardCounter += 1;
            currentCardId = cardTemplate.cardInfo.Id;
            listOfCarfInTable.Add(cardTemplate.cardInfo.Description);

            _soundEffect.Play();
            //foreach (var i in Manager.SeedList)
            //{
            //    Debug.Log (i);
            //}
        }

    }


    void RemoveFromDeck(Card card)
    {
        if (_deckManager.Deck.Contains(card))
            _deckManager.Deck.Remove(card);
        else if (_deckManager.Deck_tmp.Contains(card))
        {
            if (_deckManager.Deck_tmp.Count > 1)
            {
                _deckManager.Deck_tmp[_deckManager.Deck_tmp.IndexOf(card) - 1].canDrag = true;
            }
            _deckManager.Deck_tmp.Remove(card);

        }

    }

}
