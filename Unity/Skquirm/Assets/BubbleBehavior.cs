using UnityEngine;
using System.Collections;

public class BubbleBehavior : MonoBehaviour {

    Vector3 regular_scale;
    bool interactable = false;

    Vector3 start_scale;
    Vector3 position;

    // Use this for initialization
    void Start () {
        start_scale = Vector3.zero;
        position = transform.localPosition;
        regular_scale = transform.localScale;

        position.y = 0.86f;

        transform.localPosition = position;
        iTween.ScaleFrom(gameObject, iTween.Hash("scale", start_scale, "easetype", "spring", "time", 0.5f));
        StartCoroutine(SetInteractibleWith(0.52f, true));
	}

    IEnumerator SetInteractibleWith(float time, bool value) {
        yield return new WaitForSeconds(time);
        interactable = value;
    }


    public void BubbleReaction() { 

        if (interactable)
        {
            iTween.Stop(gameObject);
            iTween.ScaleTo(gameObject, iTween.Hash("scale", 0.9f * regular_scale, "time", 0.1f));
            iTween.ScaleTo(gameObject, iTween.Hash("scale", 1.1f*regular_scale, "easetype", "spring", "time", 0.3f, "delay", 0.12f));
            iTween.ScaleTo(gameObject, iTween.Hash("scale", regular_scale, "easetype", "spring", "delay", 0.4f, "time", 0.3f));
        }
        
    }

    public void DestroyBarrier() {
        interactable = false;
        iTween.Stop(gameObject);

        transform.localScale = regular_scale;

        transform.localPosition = position;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", start_scale, "easetype", "spring", "time", 0.5f));
        Destroy(gameObject, 0.5f);
    }
}
