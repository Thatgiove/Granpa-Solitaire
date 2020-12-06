using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoading : MonoBehaviour
{
    [SerializeField] Image _ProgressBar;
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync((int)Utility.Scene.GameTable);
        //fill la progress
        while (gameLevel.progress < 1)
        {
            _ProgressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
