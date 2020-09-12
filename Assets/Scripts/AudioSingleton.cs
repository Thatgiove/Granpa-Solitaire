using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSingleton : Singleton<AudioSingleton>
{

    private void Start()
    {
        PlayMusic();
    }


    public void PlayMusic()
    {
        AudioSource music = GameObject.Find("Music").GetComponent<AudioSource>();
        Image btnImage = GameObject.Find("AudioButton").GetComponent<Image>();



        if (Manager.musicPlaying)
        {
            music.Play();
            btnImage.sprite = Resources.Load<Sprite>("Buttons/SoundOn");
        }
        else
        {
            music.Pause();
            btnImage.sprite = Resources.Load<Sprite>("Buttons/SoundOff");
        }
    }

}