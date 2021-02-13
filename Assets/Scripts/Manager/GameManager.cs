using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool AlreadyPlayed = false;
    public static string PrincipalCardSeed;
    public static int PrincipalCardValue;
    public static List<string> PrincipalCardSeedList = new List<string>(); //lista di stringhe che tiene conto del seme
    public static bool ShouldPlaySound = false;
    public static bool MatrixEmpty = false;
    public static bool DeckEmpty = false;
    public static AudioSource _audioSource;
    void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }
    void Update()
    {
        if (MatrixEmpty && DeckEmpty)
        {
            var audioclip = (AudioClip)Resources.Load("Audio/success_2");
            if(audioclip && ShouldPlaySound && !AlreadyPlayed)
            {
                //TODO bisogna disattivare la musica alla vittoria
                GameObject.Find("Music").GetComponent<AudioSource>().Stop();
                _audioSource.PlayOneShot(audioclip);
            }    
                
            GameObject canvas = GameObject.Find("MainCanvas").gameObject;
            var victoryText = canvas.transform.Find("GameOverText").gameObject;
            if (victoryText)
                victoryText.SetActive(true);

            AlreadyPlayed = true; 
        }
    }
}