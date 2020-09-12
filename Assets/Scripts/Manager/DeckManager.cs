using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/******************
 * classe che si occupa dell'avanzamento delle carte nel mazzo
 * seleziono tre carte dal mazzo e le metto in un mazzo temporaneo 
 * quando finiscono inverto l'operazione
 * 
 * 
 * **************/

public class DeckManager : MonoBehaviour
{

    List<Card> CardsSelected;

    public List<Card> Deck;
    public List<Card> Deck_tmp;


    float i = 0.05f,
          j = 0.02f;

    const float OFFSET_X = 0.02f,
                OFFSET_Z = 0.01f;

    Vector3 DECK_POSITION = new Vector3(0, -2, -1f),
            OTHER_DECK_POSITION = new Vector3(1, -2, -1.5f), // posizione a lato del principal deck
            OTHER_DECK_POSITION_TMP = new Vector3(1, -2, -1.5f); // posizione a lato del principal deck




    void Start()
    {
        Deck = GameObject.Find("SceneController").GetComponent<SceneController>().cardDeck;
        foreach (var card in Deck)
        {
            card.canDrag = false; //le carte  del mazzo non sono selezionabili
        }
    }

     
    public void SwipeCardDeck()
    {


        //TODO -- creare metodo condiviso
        if (Deck.Count > 0)
        {

            CardsSelected = Deck.Where(x => !(x.isPrincipalCard && x.canPutOnTable))
                                .Skip(Deck.Count - 3)
                                .ToList(); //seleziono le ultime 3 carte


            CardsSelected.ForEach(cards => Deck.Remove(cards));//le rimuovo dal mazzo
            CardsSelected.Reverse(); //le inverto

            Deck_tmp.AddRange(CardsSelected);//le aggiungo al  mazzo tmp


            foreach (var card in Deck_tmp)
            {
                card.canDrag = false;
                Deck_tmp.Last().canDrag = true;
                CalculateOffSet(card);
            }

        }

        else
        {
            i = 0;
            j = 0;
            CardsSelected = Deck_tmp.Where(x => !(x.isPrincipalCard && x.canPutOnTable))
                                    .Skip(3)
                                    .ToList();

            CardsSelected.ForEach(item => Deck_tmp.Remove(item));  //rimuovo le carte restanti oltre le tre
            Deck.AddRange(CardsSelected);

            foreach (Card card in Deck)
            {
                card.canDrag = false;
                card.transform.position = DECK_POSITION;
            }

            OTHER_DECK_POSITION_TMP = OTHER_DECK_POSITION; //reset della nuova posizione

            foreach (Card card in Deck_tmp)
            {
                card.canDrag = false;
                Deck_tmp.Last().canDrag = true;
                CalculateOffSet(card);

            }
        }
    }



    void CalculateOffSet(Card card)
    {
        card.transform.position = OTHER_DECK_POSITION_TMP;

        float posX = (OFFSET_X * i) + card.transform.position.x;
        float posZ = -(OFFSET_Z * j) + card.transform.position.z;

        card.transform.position = new Vector3(posX, card.transform.position.y, posZ);
        i += 0.09f;
        j += 0.01f;
    }





    public void RemoveCardFromDecks(Card card)
    {
        if (Deck.Contains(card))
            Deck.Remove(card);

        else if (Deck_tmp.Contains(card))
        {
            if (Deck_tmp.Count > 1)
            {
                Deck_tmp[Deck_tmp.IndexOf(card) - 1].canDrag = true;
            }
            Deck_tmp.Remove(card);
        }

    }
}


