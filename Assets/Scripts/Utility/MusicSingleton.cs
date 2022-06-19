using UnityEngine;

public class MusicSingleton : Singleton<MusicSingleton>
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayMusic(bool isMusicPlaying)
    {
        if (!audioSource) return;

        if (isMusicPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}