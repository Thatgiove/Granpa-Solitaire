using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    [SerializeField] private Card cardTemplate;
    [SerializeField] private List<CardInfo> CardInfoList;


    public List<Card> cardDeck;
    private List<Card> cardListInMatrix;
    public List<List<Card>> matrix = new List<List<Card>>();

    int row = 4, 
        col = 4,  
        z = 0,
        k = 0;

    public const float OFFSET_X = 0.8f, 
                OFFSET_Y = 0.1f,
                OFFSET_Z = 0.01f;


    void Awake()
    {
        List<Card> CardList = CreateCardsWithInfo(); //crea carte con scriptable object
        List<Card> CardShuffled = ShuffleCard(CardList); //mischia

        this.SplitCards(CardShuffled); 

        this.SetDeckPosition();
        this.SetMatrixPosition();
        this.SetPrincipalCard();


    }



    void SplitCards(List<Card> cardShuffled)
    {
        try
        {
            List<List<Card>> listSplitted = Split<Card>(cardShuffled, 24); //divido tutte le carte e ricavo...
            cardDeck = listSplitted.ElementAt(0); //il mazzo...
            cardListInMatrix = listSplitted.ElementAt(1); //e le carte della matrice 
            matrix = Split<Card>(cardListInMatrix, 4); //creo la matrice superiore
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    void SetDeckPosition()
    {
        try
        {
            foreach (Card card in cardDeck)//posizione e offset asse z del mazzo
            {
                card.transform.position = new Vector3(0, -1.85f, 0.5f);

                float posZ = -(OFFSET_Z * z) + gameObject.transform.position.z;
                card.transform.position = new Vector3(0, -1.85f, posZ);
                z++;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    void SetMatrixPosition()
    {

        try
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Card _card = cardListInMatrix[k];

                    _card.isMatrix = true;
                    gameObject.transform.position = new Vector3(-1.3f, 1.9f, -0.8f);//definisco una posizione iniziale

                    float posX = (OFFSET_X * i) + gameObject.transform.position.x;
                    float posY = -(OFFSET_Y * j) + gameObject.transform.position.y;
                    float posZ = -(OFFSET_Z * z) + gameObject.transform.position.z;

                    _card.transform.position = new Vector3(posX, posY, posZ);

                    k++;
                    z++;
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    void SetPrincipalCard()
    {
        try
        {
            Card principalCard = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count)]; //definisco la principalCard estraendola dal mazzo
            principalCard.transform.position = new Vector3(-3.79f, -0.3f, -0.5f);
            principalCard.isPrincipalCard = true;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

   

    //TODO spostare nel global?
    List<Card> ShuffleCard(List<Card> cardList)
    {
        List<Card> shuffledList = new List<Card>();

        for (int i = 0; i < cardList.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, cardList.Count);

            Card value = cardList[randomIndex];
            cardList[randomIndex] = cardList[i];
            cardList[i] = value;

            shuffledList.Add(value);
        }

        return shuffledList;
    }
    List<Card> CreateCardsWithInfo()
    {

        List<Card> cardList = new List<Card>();
        //cicla sulla lista delle card info
        for (int i = 0; i < CardInfoList.Count; i++)
        {
            Card cardClone;
            //creo un card template
            //l'oggetto va istanziato come gameObject
            cardClone = Instantiate(cardTemplate) as Card;

            //e assegno le informazioni
            cardClone.cardInfo = CardInfoList[i];
            cardList.Add(cardClone);
        }

        return cardList;

    }
    List<List<Card>> Split<T>(List<Card> cardTemplateList, int size)
    {
        var chunks = new List<List<Card>>();
        var chunkCount = cardTemplateList.Count() / size;

        if (cardTemplateList.Count % size > 0)
            chunkCount++;

        for (var i = 0; i < chunkCount; i++)
            chunks.Add(cardTemplateList.Skip(i * size).Take(size).ToList());

        return chunks;
    }




}
