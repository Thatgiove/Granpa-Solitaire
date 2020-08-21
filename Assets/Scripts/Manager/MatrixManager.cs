using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatrixManager : MonoBehaviour
{

    SceneController _sceneController;
    List<List<Card>> matrix;

    void Start()
    {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        this.matrix = _sceneController.matrix;

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
        //TODO -- sistemare  linq
        var item = (matrix.Where(c => c.Contains(cardTemplate))).FirstOrDefault();
        var card = item.Where(s => s == cardTemplate).First();

        if (item.Count > 1)
        {
            item[item.IndexOf(card) - 1].canDrag = true; //l'indice precedente 
            item.Remove(card);
        }
      
    }
}
