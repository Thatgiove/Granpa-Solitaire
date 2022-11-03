using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class MatrixManager : MonoBehaviour
{

    SceneController _sceneController;
    public List<List<Card>> matrix;

    public List<Card> cardMatrix;

    void Start()
    {
        _sceneController = FindObjectOfType<SceneController>();
        if (_sceneController)
        {
            matrix = _sceneController.matrix;
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
    public void PutInMatrix(Card card)
    {
        cardMatrix.Add(card);
    }
    public void RemoveFromMatrix(Card cardTemplate)
    {
        //TODO -- sistemare  linq e rendere array
        cardMatrix = (matrix.Where(c => c.Contains(cardTemplate))).FirstOrDefault();
        Card card = cardMatrix.Where(s => s == cardTemplate).First();

        if (cardMatrix.Count >= 1)
        {
            if (cardMatrix.IndexOf(card) - 1 != -1) //TODO FARE MEGLIO
                cardMatrix[cardMatrix.IndexOf(card) - 1].canDrag = true; //l'indice precedente 

            cardMatrix.Remove(card);
        }
        cardMatrix.Remove(card);

        if (MatrixIsEmpty())
        {
            GameManager.MatrixEmpty = true;
        }
        else
        {
            GameManager.MatrixEmpty = false;
        }
           
    }
    bool MatrixIsEmpty()
    {
        bool isEmpty = true;

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
