using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class SceneController : MonoBehaviour
{
    //posso modificare le la struct direttamente sull'ispector

    [SerializeField]
    private CardTemplate cardTemplate;

    [SerializeField]
    private List<CardInfo> CardInfoList;

    public List<CardTemplate> cardDeck;

    private int row = 4;
    private int col = 4;

    private int z = 0;
    private int k = 0;

    private const float OFFSET_X = 0.8f;
    private const float OFFSET_Y = 0.1f;
    private const float OFFSET_Z = 0.01f;

    private List<CardTemplate> matrix;

    public List<List<CardTemplate>> matrixListOflist = new List<List<CardTemplate>>();




    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("scenecontroller ");
        //creo una lista delle carte con le relative informazioni
        List<CardTemplate> cardTemplateList = CreateCardWithInfo();

        //mischio le carte 
        List<CardTemplate> cardShuffled = ShuffleCard(cardTemplateList);

         var listSplitted = Split<CardTemplate>(cardShuffled, 24);
        //var listSplitted = SplitProva<CardTemplate>(cardShuffled);

        cardDeck = listSplitted.ElementAt(0);
        matrix = listSplitted.ElementAt(1);



        //posizione e offset asse z del mazzo
        foreach (var card in cardDeck)
        {
            card.transform.position = new Vector3(0, -2f, 0.5f);

            float posZ = -(OFFSET_Z * z) + gameObject.transform.position.z;
            card.transform.position = new Vector3(0, -2f, posZ);
            z++;
        }

        //crea la matrice 4x4 superiore
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var a = matrix[k];
                a.isMatrix = true;
                //definisco una posizione iniziale
                gameObject.transform.position = new Vector3(-1.3f, 1.9f, -0.5f);

                float posX = (OFFSET_X * i) + gameObject.transform.position.x;
                float posY = -(OFFSET_Y * j) + gameObject.transform.position.y;
                float posZ = -(OFFSET_Z * z) + gameObject.transform.position.z;

                a.transform.position = new Vector3(posX, posY, posZ);



                k++;
                z++;
            }
        }

        //posiziona la matrice in una lista di liste
        matrixListOflist = Split<CardTemplate>(matrix,4);


        

        //definisco la principalCard estraendola dal mazzo
        CardTemplate principalCard = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count)];
        principalCard.transform.position = new Vector3(-3.79f, -0.3f, -0.5f);
        principalCard.isPrincipalCard = true;


    }

    void Update()
    {
       
    }
    List<CardTemplate> ShuffleCard(List<CardTemplate> cardList)
    {
        List<CardTemplate> shuffledList = new List<CardTemplate>();

        for (int i = 0; i < cardList.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, cardList.Count);

            CardTemplate value = cardList[randomIndex];
            cardList[randomIndex] = cardList[i];
            cardList[i] = value;

            shuffledList.Add(value);
        }

        return shuffledList;
    }
    List<CardTemplate> CreateCardWithInfo()
    {

        List<CardTemplate> cardList = new List<CardTemplate>();
        //cicla sulla lista delle card info
        for (int i = 0; i < CardInfoList.Count; i++)
        {
            CardTemplate cardClone;
            //creo un card template
            //l'oggetto va istanziato come gameObject
            cardClone = Instantiate(cardTemplate) as CardTemplate;

            //e assegno le informazioni
            cardClone.cardInfo = CardInfoList[i];
            cardList.Add(cardClone);
        }

        return cardList;

    }
    List<List<CardTemplate>> Split<T>(List<CardTemplate> cardTemplateList, int size)
    {
        var chunks = new List<List<CardTemplate>>();
        var chunkCount = cardTemplateList.Count() / size;

        if (cardTemplateList.Count % size > 0)
            chunkCount++;

        for (var i = 0; i < chunkCount; i++)
            chunks.Add(cardTemplateList.Skip(i * size).Take(size).ToList());

        return chunks;
    }



}
