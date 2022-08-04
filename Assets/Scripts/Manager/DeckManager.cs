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
    [SerializeField] GameObject cardBack;
    List<Card> CardsSelected;

    public List<Card> Deck;
    public List<Card> Deck_tmp; //il mazzetto sulla destra di quello principale

    float Xindex = 0.05f,
          Zindex = 0.02f;

    float  offset_X = Screen.width <= 1920 ? 1.5f : 2f,
           offset_Z = 0.01f;

    //TODO Aggiustare DeckPosition
    Vector3 DECK_POSITION = new Vector3(0, -1.89f, -1f),
            OTHER_DECK_POSITION = new Vector3(1, -1.89f, -1.5f), // posizione a lato del principal deck
            OTHER_DECK_POSITION_TMP = new Vector3(1, -1.89f, -1.5f); // posizione a lato del principal deck


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
            //seleziono le ultime 3 carte dal mazzo principale
            CardsSelected = Deck.Where(card => !(card.isPrincipalCard && card.canPutOnTable))
                                .Skip(Deck.Count - 3)
                                .ToList();

            //le rimuovo dal mazzo e le inverto
            CardsSelected.ForEach(cards => Deck.Remove(cards));
            CardsSelected.Reverse();


            //l'ultima carta prima delle 3 selezionate è sempre bloccata
            if (Deck_tmp.Count > 0)
            {
                Deck_tmp.Last().canDrag = false;
            }

            //le aggiungo al mazzo tmp a sinistra
            Deck_tmp.AddRange(CardsSelected);

            //cambio l'offset delle tre carte selezionate
            foreach (var card in CardsSelected)
            {
                CalculateOffSet(card);
            }

            //l'ultima carta prima delle 3 è sempre selezionabile
            CardsSelected.Last().canDrag = true;
        }
        //se non ci sono carte nel  mazzo
        else
        {
            Xindex = 0;
            Zindex = 0;
            CardsSelected = Deck_tmp.Where(x => !(x.isPrincipalCard && x.canPutOnTable))
                                    .Skip(3)
                                    .ToList();

            CardsSelected.ForEach(item => Deck_tmp.Remove(item));  //rimuovo le carte restanti oltre le tre
            CardsSelected.Reverse();
            Deck.AddRange(CardsSelected);

            foreach (Card card in Deck)
            {
                card.canDrag = false;
                card.transform.position = DECK_POSITION;
            }

            OTHER_DECK_POSITION_TMP = OTHER_DECK_POSITION; //reset della nuova posizione

            foreach (Card card in Deck_tmp)
            {
                CalculateOffSet(card);
            }

            Deck_tmp.Last().canDrag = true;
        }

        cardBack?.SetActive(!IsMainDeckEmpty());
    }

    void CalculateOffSet(Card card)
    {
        card.canDrag = false;

        card.transform.position = OTHER_DECK_POSITION_TMP;

        float posX = (offset_X * Xindex) + card.transform.position.x;
        float posZ = -(offset_Z * Zindex) + card.transform.position.z;

        card.transform.position = new Vector3(posX, card.transform.position.y, posZ);

        //Xindex per creare spazio tra le carte, Zindex per riuscire a prendere l'ultima
        Xindex += 0.09f;
        Zindex += 0.01f;
        
    }

    public void RemoveCardFromDecks(Card card)
    {
        if (Deck.Contains(card))
            Deck.Remove(card);

        else if (Deck_tmp.Contains(card))
        {
            if (Deck_tmp.Count > 1)
            {
                if (Deck_tmp.IndexOf(card) - 1 != -1) //TODO FARE MEGLIO
                    Deck_tmp[Deck_tmp.IndexOf(card) - 1].canDrag = true;
            }
            Deck_tmp.Remove(card);
        }
       
        if (DeckIsEmpty())
            GameManager.DeckEmpty = true;

        //quando rimuovo una carta dal mazzo 
        if (!card.isPrincipalCard && !card.isMatrix)
        {
            Xindex -= 0.09f;
        }
         
    }

    bool DeckIsEmpty()
    {
        return Deck.Count == 0 && Deck_tmp.Count == 0;
    }
    bool IsMainDeckEmpty()
    {
        return Deck.Count == 0;
    }

    [ContextMenu("DESTROY_DECK_TEST")]
    void DESTROY_DECK()
    {
        Deck.Clear();
        Deck_tmp.Clear();

        if (DeckIsEmpty())
            GameManager.DeckEmpty = true;
    }
}


