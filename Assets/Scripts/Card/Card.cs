using UnityEngine;

public class Card : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Material mat;
    Animator animator;

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

        mat = Resources.Load("Material/CardMaterial", typeof(Material)) as Material;
        
        mat?.EnableKeyword("GLOW_ON");
        mat?.EnableKeyword("SHAKEUV_ON");
        mat?.EnableKeyword("DOODLE_ON");
        
        mat?.SetFloat("_Glow", 0);

        mat?.SetFloat("_ShakeUvSpeed", 0f);
        mat?.SetFloat("_ShakeUvX", 0.5f);
        mat?.SetFloat("_ShakeUvy", 0.5f);

        mat?.SetFloat("_HandDrawnSpeed", 0f);
        mat?.SetFloat("_HandDrawnSpeed", 0f);

        _spriteRenderer.material = mat;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator)
            animator.runtimeAnimatorController = Resources.Load("Animation/CardAnimator") as RuntimeAnimatorController;

        canDrag = true;
        canPutOnTable = false;
        canPutInCol = false;
        gameObject.transform.localScale = new Vector3(0.18f, 0.18f, 0.18f);

        _spriteRenderer.sprite = cardInfo.CardImage;
    
        if (isPrincipalCard)
        {
            GameManager.PrincipalCardSeed = cardInfo.Description;
            GameManager.PrincipalCardValue = cardInfo.Id;
        }
    }
    void Update()
    {
        if (canDrag)
        {
            _spriteRenderer.material.SetFloat("_ShakeUvSpeed", 0.69f);
        }
        else
        {
            _spriteRenderer.material.SetFloat("_ShakeUvSpeed", 0f);
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
            _spriteRenderer.material.SetFloat("_HandDrawnAmount", 10f);
            _spriteRenderer.material.SetFloat("_HandDrawnSpeed", 5f);

            GameInstance.GrabCursor();
            float distanceToScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen));
            
            //per mettere in evidenza la posizione z della carta selezionata
            transform.position = new Vector3(pos_move.x, pos_move.y, pos_move.z <= -2 ? pos_move.z = -2f : pos_move.z - 1f);
        }
    }

    void OnMouseUp()
    {
        GameInstance.HandCursor();

        _spriteRenderer.material.SetFloat("_HandDrawnAmount", 0);
        _spriteRenderer.material.SetFloat("_HandDrawnSpeed", 0);

        if (!canPutOnTable)
        {
            gameObject.transform.position = this.originalPosition;
        }
    }
}
