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
        _sceneController = FindObjectOfType<SceneController>();
        if (_sceneController)
        {
            matrix = _sceneController.matrix;
            fakeMatrix = _sceneController.matrix;
        }

        TakeLastCardInMatrix(this.matrix);
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
