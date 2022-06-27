using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stabilisce le regole generali del livello
public class GameManager : MonoBehaviour
{
    bool alreadyPlayed = false;
    public static string PrincipalCardSeed;
    public static int PrincipalCardValue;
    public static List<string> PrincipalCardSeedList = new List<string>(); //lista di stringhe che tiene conto del seme

    public static bool MatrixEmpty = false;
    public static bool DeckEmpty = false;
    
    public static AudioSource audioSource;
    public static AudioSource musicSingleton;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        musicSingleton = FindObjectOfType<MusicSingleton>()?.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (MatrixEmpty && DeckEmpty)
        {
            var victoryMusic = (AudioClip)Resources.Load("Audio/success_2");
            if (victoryMusic && GameInstance.isMusicPlaying && !alreadyPlayed)
            {
                alreadyPlayed = true;
                musicSingleton?.Pause();
                audioSource.PlayOneShot(victoryMusic);
                StartCoroutine(WaitEndOfMusic(victoryMusic));
            }

            GameObject canvas = GameObject.Find("MainCanvas").gameObject;
            var victoryText = canvas.transform.Find("GameOverText").gameObject;
            if (victoryText)
                victoryText.SetActive(true);
        }
    }


    //Se la musica è attiva la metto in pausa e riprendo
    //dopo aver riprodotto quella della vittoria
    IEnumerator WaitEndOfMusic(AudioClip victoryMusic)
    {
        yield return new WaitForSeconds(victoryMusic.length + 0.5f);
        musicSingleton?.Play();
    }
}
