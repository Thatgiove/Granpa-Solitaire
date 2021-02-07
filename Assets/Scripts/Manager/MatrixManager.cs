using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class MatrixManager : MonoBehaviour
{

    SceneController _sceneController;
    List<List<Card>> matrix;
    List<List<Card>> fakeMatrix;

    void Start()
    {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        this.matrix = _sceneController.matrix;
        this.fakeMatrix = _sceneController.matrix;

        TakeLastCardInMatrix(this.matrix);
    }

    [ContextMenu("DESTROY_MATRIX_TEST")]
    void DESTROY_MATRIX()
    {

        
            for (int j = 0; j < fakeMatrix[0].Count; j++)
            {
            fakeMatrix[0].Clear();
            }
        for (int j = 0; j < fakeMatrix[1].Count; j++)
            {
            fakeMatrix[1].Clear();
            }
        for (int j = 0; j < fakeMatrix[2].Count; j++)
            {
            fakeMatrix[2].Clear();
        }
        for (int j = 0; j < fakeMatrix[3].Count; j++)
            {
            fakeMatrix[3].Clear();
        }
        

        if (MatrixIsEmpty())
            GameManager.MatrixEmpty = true;  
    }

    public void TakeLastCardInMatrix(List<List<Card>> listOfLists)
    {
        foreach (var item in listOfLists)
        {
            foreach (var i in item)
            {
                i.canDrag = false;
                item.Last().canDrag = true;
            }
        }

    }

    public void RemoveFromMatrix(Card cardTemplate)
    {
        //TODO -- sistemare  linq e rendere array
        List<Card> _cardList = (matrix.Where(c => c.Contains(cardTemplate))).FirstOrDefault();
        Card card = _cardList.Where(s => s == cardTemplate).First();

        if (_cardList.Count >= 1)
        {
            if (_cardList.IndexOf(card) - 1 != -1) //TODO FARE MEGLIO
                _cardList[_cardList.IndexOf(card) - 1].canDrag = true; //l'indice precedente 

            _cardList.Remove(card);
        }
        _cardList.Remove(card);
       
        if (MatrixIsEmpty())
            GameManager.MatrixEmpty = true;
    }
    bool MatrixIsEmpty()
    {
        bool isEmpty = true;
        //return matrix.All(el => el.Count == 0);
        for (int i = 0; i < matrix.Count; i++)
        {
            if (matrix[i].Count != 0)
            {
                return false;
            }
        }
        return isEmpty;
    }
}
