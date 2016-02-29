using UnityEngine;
using System.Collections;

public class BarrierOffAnimation : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Vector3 start_scale = Vector3.zero;
        Vector3 position = transform.localPosition;

        position.y = 0.86f;

        transform.localPosition = position;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", start_scale, "easetype", "spring", "time", 0.5f));
        Destroy(this);
    }
}
