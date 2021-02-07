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

    void Update()
    {
        if (MatrixEmpty && DeckEmpty)
        {
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            var audioclip = (AudioClip)Resources.Load("Audio/success");
            if(audioclip && ShouldPlaySound && !AlreadyPlayed)
            {
                audio.PlayOneShot(audioclip);
                AlreadyPlayed = true; // TOFO mettere fuori
            }
                
            GameObject canvas = GameObject.Find("MainCanvas").gameObject;
            var victoryText = canvas.transform.Find("GameOverText").gameObject;
            if (victoryText)
                victoryText.SetActive(true);
        }
    }
}