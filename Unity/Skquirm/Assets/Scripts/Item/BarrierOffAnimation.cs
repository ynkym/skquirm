using UnityEngine;
using System.Collections;

public class BarrierOffAnimation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 start_scale = Vector3.zero;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", start_scale, "easetype", "spring", "time", 0.5f));
        Destroy(this);
    }
}
