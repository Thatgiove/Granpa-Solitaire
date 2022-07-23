using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField] private Card cardTemplate;
    [SerializeField] private List<CardInfo> CardInfoList;

    [SerializeField] private GameObject firstCardPosition;
    [SerializeField] private GameObject matrixCardPosition;


    public List<Card> cardDeck;
    private List<Card> cardListInMatrix;
    public List<List<Card>> matrix = new List<List<Card>>();

    int row = 4, 
        col = 4,  
        z = 0,
        k = 0;

    public const float MATRIX_OFFSET_X = 0.88f,
                       MATRIX_OFFSET_Y = 0.07f,
                       MATRIX_OFFSET_Z = 0.01f;


    void Awake()
    {
        GenerateCardsPositionOnTable();

        ////////////TODO implementare un finder generico nel global
        GameManager.DeckEmpty = false;
        GameManager.MatrixEmpty = false;
        GameObject canvas = GameObject.Find("MainCanvas").gameObject;
        var victoryText = canvas.transform.Find("GameOverText").gameObject;
        if (victoryText)
            victoryText.SetActive(false);
        ////////////TODO implementare un finder generico nel global
        ///
        List<Card> CardList = CreateCardsWithInfo(); //crea carte con scriptable object
        List<Card> CardShuffled = ShuffleCard(CardList); //mischia

        SplitCards(CardShuffled);
       
        SetDeckPosition();
        
        SetMatrixPosition();
        
        SetPrincipalCard();
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
                card.transform.position = new Vector3(0, -1.89f, 0.5f);

                float posZ = -(MATRIX_OFFSET_Z * z) + gameObject.transform.position.z;
                card.transform.position = new Vector3(0, -1.89f, posZ);
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
                   
                    if(matrixCardPosition)
                    gameObject.transform.position = matrixCardPosition.transform.position;//definisco una posizione iniziale

                    float posX = (MATRIX_OFFSET_X * i) + gameObject.transform.position.x;
                    float posY = -(MATRIX_OFFSET_Y * j) + gameObject.transform.position.y;
                    float posZ = -(MATRIX_OFFSET_Z * z) + gameObject.transform.position.z;

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

            //la carta principale viene sempre posizionata come prima
            //firstCardPosition è l'oggetto table_position_1 nella scena
            if (firstCardPosition)
            {
                principalCard.transform.position = firstCardPosition.transform.position;
            }
            else
            {
                principalCard.transform.position = Vector3.zero;
            }
            
            principalCard.isPrincipalCard = true;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    //genero 9 cardPosition con un offset x di 1
    //una è già sul tavolo
    void GenerateCardsPositionOnTable()
    {
        if (firstCardPosition)
        {
            var startPosition = firstCardPosition.transform.position;

            for (int i = 0; i < 9; i++)
            {
                var tablePos = Instantiate(firstCardPosition);
                tablePos.transform.position = new Vector3(startPosition.x + 0.88f, startPosition.y, 0);
                startPosition = tablePos.transform.position;
            }
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
            cardClone.name = cardClone.cardInfo.name;
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
