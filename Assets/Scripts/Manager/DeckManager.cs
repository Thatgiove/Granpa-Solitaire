using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/******************
 * classe che si occupa dell'avanzamento delle carte nel mazzo
 * seleziono tre carte dal mazzo e le metto in un mazzo temporaneo 
 * quando finiscono inverto l'operazione
 * **************/

public class DeckManager : MonoBehaviour
{
    public GameObject deckButton;
    [SerializeField] GameObject secondDeckPosition;

    List<Card> CardsSelected;

    public List<Card> Deck;
    public List<Card> Deck_tmp; //il mazzetto sulla destra di quello principale

    float Xindex = 0.05f,
          Zindex = 0.02f;

    float  offset_X = 1.5f ,
           offset_Z = 0.01f;

    void Start()
    {

    }

    public void BlockDeck()
    {
        foreach (var card in Deck)
        {
            card.canDrag = false; //le carte  del mazzo non sono selezionabili
        }
    }
    public void SwipeCardDeck()
    {
        if (Deck.Count <= 0)
        {
            Xindex = 0.05f;
            Zindex = 0.02f;
            Deck.AddRange(Deck_tmp);
            Deck_tmp.Clear();
            Deck.Reverse();
            SetDeckPosition();
            BlockDeck();

            if (!CalculateDeckMoves(Deck))
            {
                print("SEI BLOCCATO STRONZO!!!!!!!!!!!!!!!!!");
            };
        }

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

        SetCardBackAlpha(IsMainDeckEmpty());
    }
    public void SetDeckPosition()
    {
        var z = 0;

        foreach (Card card in Deck)//posizione e offset asse z del mazzo
        {
            if (card.isPrincipalCard) continue;

            card.transform.position = deckButton.transform.position;

            float posZ = deckButton.transform.position.z + (offset_Z * z);
            card.transform.position = new Vector3(
                deckButton.transform.position.x,
                deckButton.transform.position.y,
                posZ);
            z++;
        }
    }

    //per calcolare le possibili mosse nel mazzo, scorro di 3 partendo dalla cima
    //e verifico se per ogni tableManager nel tavolo da gioco (10)
    //posso mettere in riga o in colonna
    public bool CalculateDeckMoves(List<Card> _deck)
    {
        var tableManagerList = FindObjectsOfType<TableManager>();

        for (int i = _deck.Count - 3; i > 0; i -= 3)
        {
            foreach (var tm in tableManagerList)
            {
                if (!tm.isMatrix && (tm.CanPutInRow(_deck[0]) ||
                                     tm.CanPutInCol(_deck[0]) ||
                                     tm.CanPutInRow(_deck[i]) ||
                                     tm.CanPutInCol(_deck[i])))
                {
                    return true;
                }
            }
        }

        return false;
    }

    void CalculateOffSet(Card card)
    {
        card.canDrag = false;

        card.transform.position = secondDeckPosition.transform.position;

        float posX = (offset_X * Xindex) + card.transform.position.x;
        float posZ = -(offset_Z * Zindex) + card.transform.position.z;

        card.transform.position = new Vector3(posX, card.transform.position.y, posZ);

        //Xindex per creare spazio tra le carte, Zindex per riuscire a prendere l'ultima
        Xindex += 0.08f;
        Zindex += 0.04f;
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
    void SetCardBackAlpha(bool isMainDeckEmpty)
    {
        var img = deckButton.GetComponent<Image>();
        if (img == null) return;

        if (isMainDeckEmpty)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, .3f); ;
        }
        else
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f); ;
        }
    }
}


