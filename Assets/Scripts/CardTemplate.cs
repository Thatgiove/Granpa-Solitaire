using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTemplate : MonoBehaviour
{
    
    public CardInfo cardInfo;
   
    public bool canDrag;

    public bool canPutOnTable;
    public bool canPutInCol;
    public bool isMatrix;

    public bool isPrincipalCard;

    //TODO rendere private
    public int cardId;
    public string cardDescription;
    public string principalCardDescription;


    SpriteRenderer m_SpriteRenderer;
    Vector3 originalPos;

    void Start()
    {
        //Debug.Log("CARdtemplate ");
        canDrag = true;
        canPutOnTable = false;
        canPutInCol = false;

        //prende lo SpriteRenderer component e assegna lo sprite 
        //della card info
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
       
        Vector3 cardScale = new Vector3(0.2f,0.2f,0.2f);
        

        //imposta la scale delle carte e la position
        gameObject.transform.localScale = cardScale;


        //l'immagine del card template è uguale a quella del card info
        m_SpriteRenderer.sprite = cardInfo.CardImage;
        cardId = cardInfo.Id;
        cardDescription = cardInfo.Description;

        if (isPrincipalCard)
        {
            Manager.CardSeed = cardDescription;
            Manager.CardValue = cardId;
           
           // Debug.Log("Manager.SEED = " + Manager.CardSeed + Manager.CardValue);
        }


    }

    void Update()
    {
        
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }

    public void OnMouseDown()
    {
        if (isPrincipalCard)
            canDrag = false;
        originalPos = gameObject.transform.position;
 
    }

    //trascina oggetto
    void OnMouseDrag()
    {
        if (canDrag)
        {
            float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z);
        }
    
    }

    void OnMouseUp()
    {
        if (!canPutOnTable)
        {
            gameObject.transform.position = originalPos;
        }
    }

}
