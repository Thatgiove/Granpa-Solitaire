using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// SceneController crea i mazzi e imposta la posizione della carte
/// </summary>
[Serializable]
public class SceneController : MonoBehaviour
{

    [SerializeField] private Card cardTemplate;
    [SerializeField] private List<CardInfo> CardInfoList;

    [SerializeField] private GameObject firstCardPosition;
    [SerializeField] private GameObject m1Position;
    [SerializeField] private GameObject m2Position;
    [SerializeField] private GameObject m3Position;
    [SerializeField] private GameObject m4Position;
    public GameObject deckCardPosition;

    [SerializeField] TutorialManager tutorialManager;

    public List<Card> cardDeck;
    private List<Card> cardListInMatrix;
    public List<List<Card>> matrix = new List<List<Card>>();


    public GameObject mainCanvas; 



    public const float MATRIX_OFFSET_Y = .08f,
                       MATRIX_OFFSET_Z = .2f;


    void Awake()
    {
        ////////////TODO implementare un finder generico nel global
        GameManager.DeckEmpty = false;
        GameManager.MatrixEmpty = false;
        GameObject canvas = GameObject.Find("MainCanvas").gameObject;
        var victoryText = canvas.transform.Find("GameOverText").gameObject;
        
        if (victoryText)
            victoryText.SetActive(false);
        ////////////TODO implementare un finder generico nel global
  
        List<Card> CardList = CreateCardsWithInfo(); //crea carte con scriptable object
        List<Card> CardShuffled = ShuffleCard(CardList); //mischia

        SplitCards(CardShuffled);

        StartCoroutine(SetPrincipalCard());
        StartCoroutine(SetMatrixPosition());
        StartCoroutine(SetDeckPosition());
    }

    void Start()
    {
        if (GameInstance.isTutorialMode && !GameInstance.isFirstRuleSeen)
        {
            tutorialManager?.OpenTutorialPanel(0);
            GameInstance.isFirstRuleSeen = true;
        }
    }


    IEnumerator SetPrincipalCard()
    {
        if (!firstCardPosition) yield return null;

        Card principalCard = cardDeck[UnityEngine.Random.Range(0, cardDeck.Count)]; //definisco la principalCard estraendola dal mazzo
        principalCard.isPrincipalCard = true;
        GameInstance.principalCard = principalCard;
  
        yield return new WaitForEndOfFrame();
        principalCard.transform.position = firstCardPosition.transform.position;
    }
    IEnumerator SetMatrixPosition()
    {
        if (!m1Position || !m2Position ||  !m3Position ||  !m4Position) yield return null;
        yield return new WaitForEndOfFrame();

        var m1 = matrix[0];
        var m2 = matrix[1];
        var m3 = matrix[2];
        var m4 = matrix[3];

        for (int i = 0; i < m1.Count; i++)
        {
            float posY = -(MATRIX_OFFSET_Y * i) + m1Position.transform.position.y;
            float posZ = -(MATRIX_OFFSET_Z * i) + m1Position.transform.position.z;
            m1[i].transform.position = new Vector3(m1Position.transform.position.x, posY, posZ);
            m1[i].isMatrix = true;
        }
        
        for (int i = 0; i < m2.Count; i++)
        {
            float posY = -(MATRIX_OFFSET_Y * i) + m2Position.transform.position.y;
            float posZ = -(MATRIX_OFFSET_Z * i) + m2Position.transform.position.z;
            m2[i].transform.position = new Vector3(m2Position.transform.position.x, posY, posZ);
            m2[i].isMatrix = true;
        }
        
        for (int i = 0; i < m3.Count; i++)
        {
            float posY = -(MATRIX_OFFSET_Y * i) + m3Position.transform.position.y;
            float posZ = -(MATRIX_OFFSET_Z * i) + m3Position.transform.position.z;
            m3[i].transform.position = new Vector3(m3Position.transform.position.x, posY, posZ);
            m3[i].isMatrix = true;
        }
      
        for (int i = 0; i < m4.Count; i++)
        {
            float posY = -(MATRIX_OFFSET_Y * i) + m4Position.transform.position.y;
            float posZ = -(MATRIX_OFFSET_Z * i) + m4Position.transform.position.z;
            m4[i].transform.position = new Vector3(m4Position.transform.position.x, posY, posZ);
            m4[i].isMatrix = true;
        }
    }

    IEnumerator SetDeckPosition()
    {
        if (!deckCardPosition) yield return null;
        yield return new WaitForEndOfFrame();

        var z = 0;

        foreach (Card card in cardDeck)//posizione e offset asse z del mazzo
        {
            if (card.isPrincipalCard) continue;

            card.transform.position = deckCardPosition.transform.position;

            float posZ = deckCardPosition.transform.position.z  + (MATRIX_OFFSET_Z * z);
            card.transform.position = new Vector3(
                deckCardPosition.transform.position.x,
                deckCardPosition.transform.position.y,
                posZ);
            z++;
        }

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
            cardClone = Instantiate(cardTemplate);

            //cardClone.gameObject.AddComponent<AllIn1Shader>();
        
            cardClone.gameObject.AddComponent<Animator>();

            //e assegno le informazioni
            cardClone.cardInfo = CardInfoList[i];
            cardClone.name = cardClone.cardInfo.name;
            if (mainCanvas)
                cardClone.gameObject.transform.parent = mainCanvas.transform;
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
