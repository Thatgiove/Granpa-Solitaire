using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{


    public List<CardTemplate> Selected;
    public List<CardTemplate> Deck;
    public List<CardTemplate> OtherDeck;
    public SceneController sceneController;
 
    private const float OFFSET_X = 0.02f;
    private const float OFFSET_Z = 0.01f;


    private float i = 0.05f;
    private float j = 0.02f;

    private Vector3 DECK_POSITION = new Vector3(0, -2, 0.5f);
    private Vector3 OTHER_DECK_POSITION = new Vector3(1, -2, 0.5f); // posizione a lato del principal deck

    private Vector3 OtherDeckNewPosition = new Vector3(1, -2, 0.5f); // posizione a lato del principal deck


    void Start()
    {
        var sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        Deck = sceneController.cardDeck;

        foreach (var card in Deck)
        {
            card.canDrag = false; //le carte  del mazzo non sono selezionabili
        }

    }

    void Update()
    {

    }

    public void PrintCardDeck()
    {

        if (Deck.Count > 0)
        {
            Selected = Deck.Where(x => !(x.isPrincipalCard && x.canPutOnTable)).Skip(Deck.Count - 3).ToList(); //seleziono le ultime 3 carte
            Selected.ForEach(item => Deck.Remove(item));//le rimuovo dal mazzo
            Selected.Reverse();//le inverto

            OtherDeck.AddRange(Selected);//le aggiungo al nuovo mazzo

           
            foreach (var card in OtherDeck)
            {
                card.canDrag = false;
                OtherDeck.Last().canDrag = true;
                CalculateOffSet(card);
            }

        }

        else 
        { 
            i = 0;
            j = 0; 
            Selected = OtherDeck.Where(x => !(x.isPrincipalCard && x.canPutOnTable)).Skip(3).ToList(); 
            Selected.ForEach(item => OtherDeck.Remove(item));  //rimuovo le carte restanti oltre le tre
            Deck.AddRange(Selected);

            foreach (var item in Deck)
            {
                item.canDrag = false;
                item.transform.position = DECK_POSITION;
            }
            
            OtherDeckNewPosition = OTHER_DECK_POSITION; //reset della nuova posizione

            foreach (var item in OtherDeck)
            {
                item.canDrag = false;
                OtherDeck.Last().canDrag = true;
                CalculateOffSet(item);

            }

        }
       

    }
    //la posizione non si resetta
    private void CalculateOffSet(CardTemplate card)
    {
        card.transform.position = OtherDeckNewPosition;

        float posX = (OFFSET_X * i) + card.transform.position.x;
        float posZ = -(OFFSET_Z * j) + card.transform.position.z;

        card.transform.position = new Vector3(posX, card.transform.position.y, posZ);
        i += 0.09f;
        j += 0.01f;
    }

}
