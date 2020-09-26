using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSingleton : Singleton<AudioSingleton>
{
    AudioSource music;
    Image btnImage;

    void Start()
    {
        music = GameObject.Find("Music").GetComponent<AudioSource>();
        btnImage = GameObject.Find("AudioIcon").GetComponent<Image>();

        PlayMusic();

    }


    public void PlayMusic()
    {

        if (Manager.PlayMusic)
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