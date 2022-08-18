using UnityEngine;
using UnityEngine.UI;

public class DeckButton : MonoBehaviour
{
    Material mat;
    float value = 0;
    float valueFI = 0;
    
    float shineAmount = 0.8f;
    float fishEyeAmount = 0.4f;

    bool toRx;
    bool toRxFI;

    void Start()
    {
        mat = GetComponent<Image>().material;
    }

    void FixedUpdate()
    {
        if (mat.GetFloat("_ShineLocation") <= 0) { toRx = true; }
        else if (mat.GetFloat("_ShineLocation") >= 1) { toRx = false; }

        if (mat.GetFloat("_FishEyeUvAmount") <= 0) { toRxFI = true; }
        else if (mat.GetFloat("_FishEyeUvAmount") >= 0.14) { toRxFI = false; }

        if (toRx)
            value += shineAmount;
        else
            value -= shineAmount;

        if (toRxFI)
            valueFI += fishEyeAmount;
        else
            valueFI -= fishEyeAmount;


        mat.SetFloat("_ShineLocation", value * Time.deltaTime);
        mat.SetFloat("_FishEyeUvAmount", valueFI * Time.deltaTime);
    }
}
