using UnityEngine;

[CreateAssetMenu (fileName = "New CardInfo", menuName = "Card Info", order = 51)]

public class CardInfo : ScriptableObject
{

    [SerializeField]
    private int _Id ;
    public int Id
    {
        get
        {
            return _Id;
        }
    }

    [SerializeField]
    private string _Description;
    public string Description
    {
        get
        {
            return _Description;
        }
    }

    [SerializeField]
    private Sprite _CardImage ;
    public Sprite CardImage
    {
        get
        {
            return _CardImage;
        }
    }

}
