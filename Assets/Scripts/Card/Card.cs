using UnityEngine;

public class Card : MonoBehaviour
{

    public CardInfo cardInfo; //scriptable-object
    Vector3 originalPosition;

    public bool canDrag;
    public bool canPutOnTable;
    public bool canPutInCol;
    public bool isMatrix;
    public bool isPrincipalCard;


    void Start()
    {
        canDrag = true;
        canPutOnTable = false;
        canPutInCol = false;
        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); 

        GetComponent<SpriteRenderer>().sprite = cardInfo.CardImage; 

        if (isPrincipalCard)
        {
            Manager.PrincipalCardSeed = cardInfo.Description;
            Manager.PrincipalCardValue = cardInfo.Id;
        }
    }



    public void OnMouseDown()
    {
        if (isPrincipalCard)
            canDrag = false;
        this.originalPosition = gameObject.transform.position;
 
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
            gameObject.transform.position = this.originalPosition;
        }
    }

}
