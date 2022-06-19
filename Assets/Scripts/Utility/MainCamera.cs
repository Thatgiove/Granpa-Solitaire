using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MainCamera : MonoBehaviour
{
    //attiva/disattiva il postProcessing 
    void Start()
    {
        var ppl = GetComponent<PostProcessLayer>();
        if(ppl)
        {
            ppl.enabled = GameInstance.isHighQuality;
        }
    }


}
