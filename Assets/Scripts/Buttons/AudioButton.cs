using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] GameObject musicImg;
    void Start()
    {
        ToggleMusicIcon(GameInstance.isMusicPlaying);
    }
    public void ToggleMusicIcon(bool isMusicPlaying)
    {
        if (!musicImg) return;

        if (isMusicPlaying)
            musicImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/musicOn");
        else
            musicImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Buttons/musicOff");
    }
}
