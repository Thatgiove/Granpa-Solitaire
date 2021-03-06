﻿using UnityEngine;

public class Card : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Vector3 originalPosition;
    public CardInfo cardInfo; //scriptable-object
    public bool canDrag;
    public bool canPutOnTable;
    public bool canPutInCol;
    public bool isMatrix;
    public bool isPrincipalCard;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        canDrag = true;
        canPutOnTable = false;
        canPutInCol = false;
        gameObject.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f);

        _spriteRenderer.sprite = cardInfo.CardImage;
    

        if (isPrincipalCard)
        {
            GameManager.PrincipalCardSeed = cardInfo.Description;
            GameManager.PrincipalCardValue = cardInfo.Id;
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
            
            //per mettere in evidenza la posizione z della carta selezionata
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z <= -2 ? pos_move.z = -2f : pos_move.z - 1f);
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
