using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] GameObject sfxImg;
    [SerializeField] GameObject musicImg;
    [SerializeField] GameObject lowHighImg;
    [SerializeField] GameObject exitImg;
    void Start()
    {
        ToggleSfxIcon(GameInstance.isSfxPlaying);
        ToggleMusicIcon(GameInstance.isMusicPlaying);
        ToggleLowHighIcon(GameInstance.isHighQuality);
    }

    public void ToggleSfxIcon(bool isSfxPlaying)
    {
        if (!sfxImg) return;

        if (isSfxPlaying)
            sfxImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/sfxOn");
        else
            sfxImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/sfxOff");
    }

    public void ToggleMusicIcon(bool isMusicPlaying)
    {
        if (!musicImg) return;

        if (isMusicPlaying)
            musicImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/musicOn");
        else
            musicImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/musicOff");
    }

    public void ToggleLowHighIcon(bool isHighQuality)
    {
        if (!lowHighImg) return;

        if (isHighQuality)
            lowHighImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/high");
        else
            lowHighImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/low");
    }
}
