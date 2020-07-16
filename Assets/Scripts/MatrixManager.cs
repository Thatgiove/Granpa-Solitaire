using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatrixManager : MonoBehaviour
{

    SceneController _sceneController;
    List<List<CardTemplate>> listOfLists;

    // Start is called before the first frame update
    void Start()
    {
        _sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();
        listOfLists = _sceneController.matrixListOflist;
        TakeLastCardInMatrix(listOfLists);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeLastCardInMatrix(List<List<CardTemplate>> listOfLists)
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

    public void RemoveFromMatrix(CardTemplate cardTemplate)
    {
        //TODO -- sistemare  linw
        var item = (listOfLists.Where(c => c.Contains(cardTemplate))).FirstOrDefault();
        var card = item.Where(s => s == cardTemplate).First();

        if (item.Count > 1)
        {
            item[item.IndexOf(card) - 1].canDrag = true; //l'indice precedente 
            item.Remove(card);
        }
      
    }
}
